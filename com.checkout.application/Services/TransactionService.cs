
using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using com.checkout.data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.checkout.application.services
{
    public class TransactionService : ITransactionService
    {
        private readonly EFRepository _contextService;
        public TransactionService(EFRepository contextService)
        {
            _contextService = contextService;
        }

        public void CreateTransaction(Transaction entity)
        {
            _contextService.Add(entity);
        }

        public Transaction GetTransactionById(int transactionID)
        {
            return _contextService.Transactions.ToList().Find(itm => itm.TransactionID == transactionID);
        }

        public List<Transaction> GetTransactionsByMerchantID(int merchantID)
        {
            return _contextService.Transactions.ToList().FindAll(itm => itm.Merchant.Id == merchantID);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            return _contextService.UpdateTransaction(transaction);
        }
    }
}