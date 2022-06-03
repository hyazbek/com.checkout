using com.checkout.data.Model;
using System;
using System.Collections.Generic;

namespace com.checkout.application.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(Transaction entity);
        Transaction GetTransactionById(Guid transactionID);
        List<Transaction> GetAllTransactions();
        bool UpdateTransaction(Transaction transaction);
    }
}