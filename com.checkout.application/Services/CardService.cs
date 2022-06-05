

using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using com.checkout.data.Repository;
using CreditCardValidator;

namespace com.checkout.application.services
{
    public class CardService : ICardService
    {
        private readonly EFRepository _contextService;

        public CardService(EFRepository contextService)
        {
            _contextService = contextService;
        }

        public void AddCard(CardDetails card)
        {
            _contextService.Add<CardDetails>(card);
        }

        public List<CardDetails> GetAllCards()
        {
            return _contextService.GetAll<CardDetails>().ToList();
        }

        public CardDetails GetCardDetailsByID(int cardID)
        {
            return _contextService.GetAll<CardDetails>().ToList().Find(itm => itm.CardDetailsID == cardID);
        }

        public CardDetails GetCardDetailsByNumber(string cardNumber)
        {
            return _contextService.GetAll<CardDetails>().ToList().Find(itm => itm.CardNumber == cardNumber);
        }

        public bool ValidateCard(CardDetails card)
        {
            CreditCardDetector detector = new CreditCardDetector(card.CardNumber);
            return detector.IsValid();
        }
    }
}