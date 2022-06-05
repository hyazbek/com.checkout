using com.checkout.api.Controllers;
using com.checkout.api.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data.Model;
using com.checkout.tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace com.checkout.tests
{
    public class PaymentProcessorTest
    {
        private readonly PaymentController _controller;
        private readonly ITransactionService _transactionService;
        private readonly ICurrencyService _currencyService;
        private readonly ICardService _cardService;
        private readonly IBankService _bankService;
        private readonly IMerchantService _merchantService;
        private readonly IConfiguration _configuration;


        public PaymentProcessorTest()
        {

            _transactionService = new TransactionServiceFake();
            _currencyService = new CurrencyServiceFake();
            _cardService = new CardServiceFake();
            _bankService = new BankServiceFake();
            _merchantService = new MerchantServiceFake();
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
            _controller = new PaymentController(_configuration, _currencyService, _cardService, _merchantService, _transactionService, _bankService);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllTransactions()
        {
            var okResult = _controller.GetAllTransactions() as OkObjectResult;

            var transactions = Assert.IsType<List<Transaction>>(okResult.Value).Count;

            Assert.Equal(3, transactions);
        }

        [Fact]
        public void Get_WhenCalled_ReturnTransactionWithPassedGUID()
        {
            Guid test = new Guid("3c4a9475-7c59-4bfd-8a0c-3dcdd9bfdab9");

            var okResult = _controller.GetTransactionByID(test);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllCardsInTheDatabase()
        {
            var okResult = _controller.GetAllCards() as OkObjectResult;

            var transactions = Assert.IsType<List<CardDetails>>(okResult.Value).Count;

            Assert.Equal(2, transactions);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllMerchantsInTheDatabase()
        {
            var okResult = _controller.GetAllMerchants() as OkObjectResult;

            var transactions = Assert.IsType<List<Merchant>>(okResult.Value).Count;

            Assert.Equal(1, transactions);
        }

        [Fact]
        public void Post_WhenCalled_StartPaymentProcess()
        {

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = 999,
                Card = new CardDetails() { CardDetailsID = 3, CardNumber = "65432154567", Cvv = "1111", ExpiryMonth = "11", ExpiryYear = "2033", HolderName = "Unit Testing" },
                CurrencyID = 3,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result as OkObjectResult;

            Assert.IsType<Transaction>(okResult.Value);
        }

        [Fact]
        public void Post_WhenCalled_InvalidMerchantReturnsBadRequest()
        {

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = 999,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 3,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A1B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result;

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void Post_WhenCalled_InvalidCurrencyReturnsBadRequest()
        {

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = 999,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 4,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result;

            Assert.IsType<BadRequestObjectResult>(okResult);
        }

        [Fact]
        public void Post_WhenCalled_InvalidAmountReturnsBadRequest()
        {

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = -999,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 4,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result;

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
        [Fact]
        public void Post_WhenCalled_ExpiredCardReturnsBadRequest()
        {

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = 999,
                Card = new CardDetails() { CardDetailsID = 10, CardNumber = "777777777", Cvv = "1111", ExpiryMonth = "11", ExpiryYear = "2020", HolderName = "Unit Testing" },
                CurrencyID = 3,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result;

            Assert.IsType<BadRequestObjectResult>(okResult);
        }
    }
}