
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

        public List<Transaction> GetAllTransactions()
        {
            return _contextService.GetAll<Transaction>().ToList();
        }
        public Transaction? GetTransactionById(Guid transactionID)
        {
            return _contextService.Find<Transaction>(t => t.TransactionID == transactionID).FirstOrDefault();
        }

        public List<Transaction> GetTransactionsByMerchantID(Guid merchantID)
        {
            return _contextService.GetAll<Transaction>().ToList().FindAll(itm => itm.Merchant.Id == merchantID);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            return _contextService.Update<Transaction>(transaction);
        }
    }
}