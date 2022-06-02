
using com.checkout.common.Helpers;

namespace com.checkout.bank.Model
{
    public class BankResponse
    {
        public Guid BankResponseID { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
