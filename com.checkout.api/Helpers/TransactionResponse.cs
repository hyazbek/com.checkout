using com.checkout.data.Model;

namespace com.checkout.api.Helpers
{
    public class TransactionResponse
    {
        public string? Currency { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        //public Guid BankReferenceID { get; set; }
        public CardDetails? Card { get; set; }
    }
}
