using com.checkout.api.Controllers;
using com.checkout.api.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.application.services;
using com.checkout.data.Model;
using com.checkout.data.Repository;
using com.checkout.tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace com.checkout.tests
{
    public class CardServiceMoq
    {

        private readonly CardService _cardService;
        private readonly Mock<EFRepository> _efMock;

        public CardServiceMoq()
        {
            _efMock = new Mock<EFRepository>();
            _cardService = new CardService(_efMock.Object);
        }


        [Fact]
        public void Get_WhenCalled_AddsCards()
        {
            var card = new CardDetails()
            {
                CardDetailsID = 10,
                CardNumber = "777777777",
                Cvv = "777",
                ExpiryMonth = "12",
                ExpiryYear = "2020",
                HolderName = "Test Card 1 Expired"
            };

            _efMock.Setup(x => x.Add<CardDetails>(card));
            _cardService.AddCard(card);


        }

        
    }
}