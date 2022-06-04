using com.checkout.api.Helpers;
using com.checkout.application.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.application.Models;
using com.checkout.data;
using com.checkout.data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace com.checkout.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
       
        private ICardService _cardService;
        private IMerchantService _merchantService;
        private ICurrencyService _currencyService;
        private ITransactionService _transactionService;
        private IBankService _bankService;

        public PaymentController(ICurrencyService currencyService, ICardService cardService, IMerchantService merchantService, ITransactionService transactionService, IBankService bankService)
        {
            _cardService = cardService;
            _currencyService = currencyService;
            _merchantService = merchantService;
            _transactionService = transactionService;
            _bankService = bankService;
        }

        [HttpGet]
        [Route("GetAllCards")]
        public ActionResult GetAllCards()
        {
            return Ok( _cardService.GetAllCards());
        }

        [HttpGet]
        [Route("GetAllMerchants")]
        public ActionResult GetAllMerchants()
        {

            return Ok(_merchantService.GetMerchants());
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public ActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("GetTransactionByID")]
        public ActionResult GetTransactionByID(Guid transactionId)
        {

            var response = new TransactionResponse();

            var entity = _transactionService.GetTransactionById(transactionId);
            if(entity == null)
            {
                return BadRequest("Invalid Transaction");
            }
            var currency = _currencyService.GetCurrencyByID(entity.CurrencyID);
            response.Currency = currency.CurrencyCode;

            response.Amount = entity.Amount;
            
            response.Status = entity.Status;

            var card = _cardService.GetCardDetailsByID(entity.CardDetailsID);
            if (card == null)
            {
                return BadRequest("Invalid Card");
            }
            card.CardNumber = CardHelper.MaskCarNumber(card.CardNumber);
            response.Card = card;

            return Ok(response);
        }

        [HttpPost]
        [Route("ProcessTransaction")]
        public async Task<IActionResult> ProcessTransaction(PaymentRequest paymentRequest)
        {
            if(paymentRequest == null)
            {
                return BadRequest("Invalid Payment Request");
            }
            if (paymentRequest.Amount <= 0)
            {
                return BadRequest("Invalid Amount");
            }

            bool isValid = Guid.TryParse(paymentRequest.MerchantID, out Guid merchantID);
            if (!isValid)
            {
                return BadRequest("Invalid Merchant ID");
            }

            var merchant = new Merchant();
            merchant = _merchantService.GetMerchant(merchantID);
            if (merchant == null)
            {
                return BadRequest("Invalid Merchant");
            }

            var currency = new Currency();
            currency = _currencyService.GetCurrencyByID(paymentRequest.CurrencyID);
            if(currency == null)
            {
                return BadRequest("Invalid Currency");
            }

            var card = new CardDetails();
            card = _cardService.GetCardDetailsByNumber(paymentRequest.Card.CardNumber);
            if (card == null)
            {
                // add card to the system
                card = new CardDetails
                {
                    CardNumber = paymentRequest.Card.CardNumber,
                    Cvv = paymentRequest.Card.Cvv,
                    HolderName = paymentRequest.Card.HolderName,
                    ExpiryMonth = paymentRequest.Card.ExpiryMonth,
                    ExpiryYear = paymentRequest.Card.ExpiryYear,
                };
                _cardService.AddCard(card);
            }

            // create transaction object with properties gathered above
            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                Status = TransactionStatus.Created.ToString(),
                Merchant = merchant,
                CardDetails = card,
                Amount = paymentRequest.Amount,
                Currency = currency,
                StatusCode = TransactionCode.C_00001.ToString()
            };

            // save transaction in Transactions table before processing the payment with the bank.
            _transactionService.CreateTransaction(transaction);

            var unprocessedTransaction = new UnprocessedTransaction
            {
                Amount = paymentRequest.Amount,
                CardCvv = paymentRequest.Card.Cvv,
                HolderName = paymentRequest.Card.HolderName,
                CardNumber = paymentRequest.Card.CardNumber,
                ExpiryMonth = paymentRequest.Card.CardNumber,
                ExpiryYear = paymentRequest.Card.ExpiryYear
            };
            // process bank payment, hardcoded responses from the bank and updating the transaction object with bank response

            var newTransaction = _bankService.ProcessTranaction(unprocessedTransaction).Result;
            transaction.Status = newTransaction.TransactionStatus.ToString();
            transaction.StatusCode = newTransaction.TransactionCode.ToString();

            _transactionService.UpdateTransaction(transaction);

            return Ok(transaction);
        }
    }
}
