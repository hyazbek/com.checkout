using com.checkout.common.Helpers;
using System;

namespace com.checkout.common
{
    public class BankResponse
    {
        public Guid BankResponseID { get; set; }
        public TransactionStatus Status { get;set; }
    }
}