using com.checkout.api.Controllers;
using com.checkout.api.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data.Model;
using com.checkout.tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace com.checkout.tests
{
    public class PaymentProcessorTestMoq
    {
        private readonly PaymentController _controller;
        private readonly Mock<ITransactionService> _transactionServiceMock;
        private readonly Mock<ICurrencyService> _currencyServiceMock;
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<IBankService> _bankServiceMock;
        private readonly Mock<IMerchantService> _merchantServiceMock;
        private readonly IConfiguration _configuration;

        public PaymentProcessorTestMoq()
        {

            _transactionServiceMock = new Mock<ITransactionService>();
            _currencyServiceMock = new Mock<ICurrencyService>();
            _cardServiceMock = new Mock<ICardService>();
            _bankServiceMock = new Mock<IBankService>();
            _merchantServiceMock = new Mock<IMerchantService>();

            
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();

            
            _controller = new PaymentController(_configuration, _currencyServiceMock.Object, _cardServiceMock.Object, _merchantServiceMock.Object, _transactionServiceMock.Object, _bankServiceMock.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllTransactions()
        {
            var okResult = _controller.GetAllTransactions() as OkObjectResult;
            
            var transactions = Assert.IsType<List<Transaction>>(okResult?.Value).Count;

            Assert.Equal(3, transactions);
        }

        [Fact]
        public void Get_WhenCalled_ReturnTransactionWithPassedGUID()
        {
            _transactionServiceMock.Setup(itm => itm.GetTransactionById(It.IsAny<Guid>()));
            var test = new Guid("ed9a5b76-b5cc-46f6-9372-7657a2812158");

            var okResult = _controller.GetTransactionByID(test);

            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllCardsInTheDatabase()
        {
            
            var okResult = _controller.GetAllCards() as OkObjectResult;

            var transactions = Assert.IsType<List<CardDetails>>(okResult?.Value).Count;

            Assert.Equal(2, transactions);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllMerchantsInTheDatabase()
        {
            var okResult = _controller.GetAllMerchants() as OkObjectResult;

            var transactions = Assert.IsType<List<Merchant>>(okResult?.Value).Count;

            Assert.Equal(1, transactions);
        }

        [Fact]
        public void Post_WhenCalled_StartPaymentProcess()
        {

            var paymentRequest = new PaymentRequest()
            {
                Amount = 999,
                Card = new CardDetails() { CardDetailsID = 3, CardNumber = "4953089013607", Cvv = "1111", ExpiryMonth = "11", ExpiryYear = "2033", HolderName = "Unit Testing" },
                CurrencyID = 3,
                MerchantID = "BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"
            };
            var okResult = _controller.ProcessTransaction(paymentRequest).Result as OkObjectResult;

            Assert.IsType<Transaction>(okResult?.Value);
        }

        [Fact]
        public void Post_WhenCalled_InvalidMerchantReturnsBadRequest()
        {

            var paymentRequest = new PaymentRequest()
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

            var paymentRequest = new PaymentRequest()
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

            var paymentRequest = new PaymentRequest()
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

            var paymentRequest = new PaymentRequest()
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