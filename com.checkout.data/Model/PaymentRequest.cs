
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.checkout.data.Model
{
    public class PaymentRequest
    {
        public int PaymentRequestID { get; set; }
        public string CurrencyCode { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public CardDetails Card { get; set; }
        public string MerchantID { get; set; }
        public string Bank { get; set; }
    }
}
