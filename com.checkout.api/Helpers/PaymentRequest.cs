
using com.checkout.data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.checkout.api.Helpers
{
    public class PaymentRequest
    {
        public int CurrencyID { get; set; }
        public decimal Amount { get; set; }
        public CardDetails Card { get; set; }
        public string MerchantID { get; set; }
    }
}
