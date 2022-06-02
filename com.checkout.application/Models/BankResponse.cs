
using com.checkout.application.Helpers;
using System;

namespace com.checkout.application.Models
{
    public class BankResponse
    {
        public Guid BankResponseID { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionCode TransactionCode { get; set; }
    }
}