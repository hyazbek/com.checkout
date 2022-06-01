using com.checkout.data.Model;

namespace com.checkout.application.Interfaces
{
    public interface ICardService
    {
        List<CardDetails> GetAllCards();
        CardDetails GetCardDetailsByNumber(string cardNumber);
        CardDetails GetCardDetailsByID(int cardID);
        void AddCard(CardDetails card);
    }
}