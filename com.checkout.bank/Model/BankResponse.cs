using com.checkout.application.Helpers;

namespace com.checkout.bank.Model
{
    public class BankResponse
    {
        public Guid BankResponseID { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionCode TransactionCode { get; set; }
    }
}
