﻿using com.checkout.api.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.common;
using com.checkout.common.Helpers;
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
        private IBankService _bankService;

        public PaymentController(CKODBContext context, ICurrencyService currencyService, ICardService cardService, IMerchantService merchantService, ITransactionService transactionService, IBankService bankService)
        {
            _context = context;
            _cardService = cardService;
            _currencyService = currencyService;
            _merchantService = merchantService;
            _transactionService = transactionService;
            _bankService = bankService;
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
                .Include(itm => itm.Merchant)
                .Include(itm => itm.CardDetails)
                .Include(itm => itm.Currency)
                .ToArrayAsync();
            

            return Ok(transactions);
        }

        [HttpGet]
        [Route("GetTransactionByID")]
        public async Task<IActionResult> GetTransactionByID(Guid transactionId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new TransactionResponse();

            var entity = _transactionService.GetTransactionById(transactionId);

            var currency = _currencyService.GetCurrencyByID(entity.CurrencyID);
            response.Currency = currency.CurrencyCode;

            response.Amount = entity.Amount;
            //response.BankReferenceID = entity.BankReferenceID;
            response.Status = entity.Status;

            var card = _cardService.GetCardDetailsByID(entity.CardDetailsID);

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


            return Ok(transaction);
        }
    }
}
