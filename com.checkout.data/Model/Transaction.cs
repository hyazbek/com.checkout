using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.checkout.data.Model
{
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public Merchant Merchant { get; set; }
        public CardDetails CardDetails { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
    }
}
