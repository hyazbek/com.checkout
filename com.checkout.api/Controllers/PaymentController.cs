using com.checkout.application;
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

        public PaymentController(CKODBContext context)
        {
            _context = context;
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

        [HttpPost]
        [Route("AddCard")]
        public async Task<IActionResult> ProcessTransaction(PaymentRequest paymentRequest)
        {
            if(paymentRequest == null)
            {
                return BadRequest("Null Reference: Payment Request");
            }
            if(paymentRequest.Merchant == null)
            {
                return BadRequest("Invalid Merchant");
            }
            if(paymentRequest.Currency == null)
            {
                return BadRequest("Invalid Currency");
            }
            if(paymentRequest.Amount <= 0)
            {
                return BadRequest("Invalid Amount");
            }
            var merchant = new Merchant();
            merchant = _merchantService.GetMerchant(paymentRequest.MerchantID);

            var card = new CardDetails();
            card = _cardService.GetCardDetailsByNumber(paymentRequest.Card);
            var transaction = new Transaction
            {
                Merchant = merchant,
                CardDetails = 

            }
        }
    }
}
