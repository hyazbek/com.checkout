﻿using com.checkout.application.Helpers;
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
        [Route("GetAllCurrencies")]
        public async Task<ActionResult<List<CardDetails>>> Get()
        {
            return Ok(await _context.Currencies.ToListAsync());
        }

        [HttpGet]
        [Route("GetAllCards")]
        public async Task<IActionResult> GetAllCards()
        {
            return Ok(await _context.Cards.ToListAsync());
        }
        [HttpGet]
        [Route("GetAllMerchants")]
        public async Task<IActionResult> GetAllMerchants()
        {
            var merchants = await _context.Merchants
                .Select(itm => itm)
                .ToArrayAsync();
            var response = merchants.Select(itm => itm);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _context.Transactions
                .Select(itm => itm)
                .ToArrayAsync();
            var response = transactions.Select(itm => itm);

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
                TransactionID = Guid.NewGuid(),
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
