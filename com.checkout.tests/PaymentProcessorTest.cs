using com.checkout.api.Controllers;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data.Model;
using com.checkout.tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;

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

        public PaymentProcessorTest()
        {

            _transactionService = new TransactionServiceFake();
            _currencyService = new CurrencyServiceFake();
            _cardService = new CardServiceFake();
            _bankService = new BankServiceFake();
            _merchantService = new MerchantServiceFake();
            _controller = new PaymentController(_currencyService, _cardService, _merchantService, _transactionService, _bankService);
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
            PaymentRequest
            var okResult = _controller.ProcessTransaction(

            var transactions = Assert.IsType<List<Merchant>>(okResult.Value).Count;

            Assert.Equal(1, transactions);
        }
    }
}