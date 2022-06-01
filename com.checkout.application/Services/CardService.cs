

using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using com.checkout.data.Repository;


namespace com.checkout.application.services
{
    public class CardService : ICardService
    {
        private readonly RepositoryService _contextService;

        public CardService(RepositoryService contextService)
        {
            _contextService = contextService;
        }

        public void AddCard(CardDetails card)
        {
            _contextService.Add(card);
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
    }
}