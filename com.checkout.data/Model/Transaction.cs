namespace com.checkout.data.Model
{
    public class Transaction
    {
        public int PaymentRequestID { get; set; }
        public Merchant Merchant { get; set; }
        public CardDetails CardDetails { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
    }
}
