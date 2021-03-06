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
       
        private readonly ICardService _cardService;
        private readonly IMerchantService _merchantService;
        private readonly ICurrencyService _currencyService;
        private readonly ITransactionService _transactionService;
        private readonly IBankService _bankService;
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration, ICurrencyService currencyService, ICardService cardService, IMerchantService merchantService, ITransactionService transactionService, IBankService bankService)
        {
            _cardService = cardService;
            _currencyService = currencyService;
            _merchantService = merchantService;
            _transactionService = transactionService;
            _bankService = bankService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/cards")]
        public ActionResult GetAllCards()
        {
            return Ok( _cardService.GetAllCards());
        }

        [HttpGet]
        [Route("/merchants")]
        public ActionResult GetAllMerchants()
        {

            return Ok(_merchantService.GetMerchants());
        }

        [HttpGet]
        [Route("/transactions")]
        public ActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions();

            return Ok(transactions);
        }

        [HttpGet]
        [Route("/transactions/{transactionId:Guid}")]
        public ActionResult GetTransactionByID(Guid transactionId)
        {

            var response = new TransactionResponse();

            var entity = _transactionService.GetTransactionById(transactionId);
            if(entity == null)
            {
                return BadRequest("Invalid Transaction");
            }

            var currency = _currencyService.GetCurrencyByID(entity.CurrencyID);
            if(currency == null)
            {
                return BadRequest("Invalid Currency");
            }

            response.Currency = currency.CurrencyCode;

            response.Amount = entity.Amount;
            
            response.Status = entity.Status;

            var card = _cardService.GetCardDetailsByID(entity.CardDetailsID);
            if (card == null || card.CardNumber == null)
            {
                return BadRequest("Invalid Card");
            }

            card.CardNumber = CardHelper.MaskCarNumber(card.CardNumber);
            response.Card = card;

            return Ok(response);
        }

        [HttpPost]
        [Route("/transactions")]
        public async Task<IActionResult> ProcessTransaction(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Invalid Payment Request");
            }
            if (paymentRequest.Card == null)
            {
                return BadRequest("Invalid Card");
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

            var merchant = _merchantService.GetMerchant(merchantID);
            if (merchant == null)
            {
                return BadRequest("Invalid Merchant");
            }

            var currency = _currencyService.GetCurrencyByID(paymentRequest.CurrencyID);
            if(currency == null)
            {
                return BadRequest("Invalid Currency");
            }
            if (paymentRequest.Card.CardNumber == null)
            {
                return BadRequest("Invalid Card");
            }
            var card = _cardService.GetCardDetailsByNumber(paymentRequest.Card.CardNumber);
            if (card != null)
            {
                if (!CardExpired(card.ExpiryMonth, card.ExpiryYear)){
                    return BadRequest("Card Expired");
                }
            }
            else
            {
                card = new CardDetails
                {
                    CardNumber = paymentRequest.Card.CardNumber,
                    Cvv = paymentRequest.Card.Cvv,
                    HolderName = paymentRequest.Card.HolderName,
                    ExpiryMonth = paymentRequest.Card.ExpiryMonth,
                    ExpiryYear = paymentRequest.Card.ExpiryYear,
                };
                
                if (!CardExpired(card.ExpiryMonth, card.ExpiryYear)){
                    return BadRequest("Card Expired");
                }
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

            var newTransaction =  _bankService.ProcessTranaction(unprocessedTransaction, _configuration["appsettings:BankApi"]).Result;
            transaction.Status = newTransaction.TransactionStatus.ToString();
            transaction.StatusCode = newTransaction.TransactionCode.ToString();

            _transactionService.UpdateTransaction(transaction);

            return await Task.FromResult(Ok(transaction));
        }

        private static bool CardExpired(string? expiryMonth, string? expiryYear)
        {
            if (expiryMonth == null || expiryYear == null) { return false; }
            

            if (DateTime.TryParse("01/" + expiryMonth + '/' + expiryYear, out DateTime dateValue))
                if (dateValue < DateTime.Now)
                    return false;
                else
                    return true;
            else
                return false;

        }
    }
}
