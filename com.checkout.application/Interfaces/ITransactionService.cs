using com.checkout.data.Model;
using System;
using System.Collections.Generic;

namespace com.checkout.application.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(Transaction entity);
        Transaction GetTransactionById(int transactionID);
        List<Transaction> GetTransactionsByMerchantID(int merchantID);
        bool UpdateTransaction(Transaction transaction);
    }
}