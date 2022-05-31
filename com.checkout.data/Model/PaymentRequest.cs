using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.data.Model
{
    public class PaymentRequest
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public CardDetails Card { get; set; }
        public string MerchantID { get; set; }
        public string Bank { get; set; }
    }
}
