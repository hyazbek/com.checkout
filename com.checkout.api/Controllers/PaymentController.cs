using com.checkout.application.Helpers;
using com.checkout.application.Interfaces;
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
        
        private readonly CKODBContext _context;
        private ICardService _cardService;
        private IMerchantService _merchantService;
        private ICurrencyService _currencyService;
        private ITransactionService _transactionService;

        public PaymentController(CKODBContext context, ICurrencyService currencyService, ICardService cardService, IMerchantService merchantService, ITransactionService transactionService)
        {
            _context = context;
            _cardService = cardService;
            _currencyService = currencyService;
            _merchantService = merchantService;
            _transactionService = transactionService;
        }

        [HttpGet]
        [Route("GetAllCards")]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _context.Cards
                .Select(itm => itm)
                .ToArrayAsync();
            var response = cards.Select(itm => itm);

            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllMerchants")]
        public async Task<IActionResult> GetAllMerchants()
        {
            var cards = await _context.Merchants
                .Select(itm => itm)
                .ToArrayAsync();
            var response = cards.Select(itm => itm);

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

            bool isValid = int.TryParse(paymentRequest.MerchantID, out int merchantID);
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
            currency = _currencyService.GetCurrencyByCode(paymentRequest.CurrencyCode);
            if(currency == null)
            {
                return BadRequest("Invalid Currency");
            }

            var card = new CardDetails();
            card = _cardService.GetCardDetailsByNumber(paymentRequest.Card.CardNumber);
            if (card == null)
            {
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

            var transaction = new Transaction
            {
                Status = TransactionStatus.Created.ToString(),
                Merchant = merchant,
                CardDetails = card,
                Amount = paymentRequest.Amount,
                Currency = currency
            };

            _transactionService.CreateTransaction(transaction);

            return Ok(transaction);
        }
    }
}
