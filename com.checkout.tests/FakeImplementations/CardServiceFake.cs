using com.checkout.application.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.tests.FakeImplementations
{
    internal class CardServiceFake : ICardService
    {
        private readonly List<CardDetails> _cards;

        public CardServiceFake()
        {
            _cards = new List<CardDetails>()
            {
                new CardDetails()
                {
                    CardDetailsID = 10,
                    CardNumber = "777777777",
                    Cvv = "777",
                    ExpiryMonth="12",
                    ExpiryYear="2025",
                    HolderName="Test Card 1"
                },
                new CardDetails()
                {
                    CardDetailsID = 11,
                    CardNumber = "8888888888",
                    Cvv = "888",
                    ExpiryMonth="12",
                    ExpiryYear="2030",
                    HolderName="Test Card 2"
                }
            };
        }

        public void AddCard(CardDetails card)
        {
            _cards.Add(card);
        }

        public List<CardDetails> GetAllCards()
        {
            return _cards;
        }

        public CardDetails GetCardDetailsByID(int cardID)
        {
            return _cards.Find(itm => itm.CardDetailsID == cardID);
        }

        public CardDetails GetCardDetailsByNumber(string cardNumber)
        {
            return _cards.Find(itm => itm.CardNumber == cardNumber);
        }
    }
}
