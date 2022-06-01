
namespace com.checkout.data.Model
{
    public class PaymentRequest
    {
        public int PaymentRequestID { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public CardDetails Card { get; set; }
        public string MerchantID { get; set; }
        public string Bank { get; set; }
    }
}
