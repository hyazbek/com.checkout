using com.checkout.data.Model;
using System;
using System.Collections.Generic;

namespace com.checkout.application
{
    public interface  ITransactionService
    {
        void CreateTransaction(Transaction entity);
        Transaction GetTransactionById(Guid transactionID);
        List<Transaction> GetTransactionsByMerchantID(Guid merchantID);
        void UpdateTransaction(Transaction transaction);
    }
}