using com.checkout.application.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.tests.FakeImplementations
{
    internal class TransactionServiceFake : ITransactionService
    {
        private readonly List<Transaction> _transactions;

        public TransactionServiceFake()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction() {
                    TransactionID = new Guid("3c4a9475-7c59-4bfd-8a0c-3dcdd9bfdab9"),
                    Amount = new Decimal(322),
                    CardDetails = new CardDetails() {
                        CardDetailsID = 10,
                        CardNumber = "6543246132",
                        Cvv = "144",
                        ExpiryMonth = "10",
                        ExpiryYear = "2025",
                        HolderName = "Test Case 1"
                    },
                    CardDetailsID = 10,
                    Currency = new Currency()
                    {
                        CurrencyCode = "QAR",
                        CurrencyName = "Qatari Riyal",
                        Id = 1
                    },
                    CurrencyID = 1,
                    Merchant = new Merchant()
                    {
                        Id = new Guid("E2305095-8E72-4F43-AC7F-BF0D5E30A83B"),
                        Country = "Lebanon",
                        Name = "Zara"
                    },
                    MerchantID = new Guid("E2305095-8E72-4F43-AC7F-BF0D5E30A83B"),
                    Status = TransactionStatus.Created.ToString(),
                    StatusCode = TransactionCode.C_00001.ToString()
                },
                new Transaction() {
                    TransactionID = new Guid("c0df6869-fc8b-4859-a090-f8e6b9016e0d"),
                    Amount = new Decimal(199),
                    CardDetails = new CardDetails() {
                        CardDetailsID = 11,
                        CardNumber = "222222222",
                        Cvv = "1444",
                        ExpiryMonth = "11",
                        ExpiryYear = "2025",
                        HolderName = "Test Case 2"
                    },
                    CardDetailsID = 11,
                    Currency = new Currency()
                    {
                        CurrencyCode = "USD",
                        CurrencyName = "US Dollar",
                        Id = 2
                    },
                    CurrencyID = 2,
                    Merchant = new Merchant()
                    {
                        Id = new Guid("BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"),
                        Country = "USA",
                        Name = "Amazon"
                    },
                    MerchantID = new Guid("BCD71F3D-6B23-4FE1-927B-FAA08A4B8908"),
                    Status = TransactionStatus.FailedInsufficientFunds.ToString(),
                    StatusCode = TransactionCode.SD_20051.ToString()
                },
                new Transaction() {
                    TransactionID = new Guid("b487fefa-67ec-447c-952f-ba4b9a7a14f7"),
                    Amount = new Decimal(100),
                    CardDetails = new CardDetails() {
                        CardDetailsID = 12,
                        CardNumber = "3333333333",
                        Cvv = "198",
                        ExpiryMonth = "02",
                        ExpiryYear = "2024",
                        HolderName = "Test Case 3"
                    },
                    CardDetailsID = 10,
                    Currency = new Currency()
                    {
                        CurrencyCode = "GBP",
                        CurrencyName = "British Pound",
                        Id = 3
                    },
                    CurrencyID = 3,
                    Merchant = new Merchant()
                    {
                        Id = new Guid("FAC164B8-0C72-4147-9EFA-4CE58C1A4DFD"),
                        Country = "UK",
                        Name = "Farfetch"
                    },
                    MerchantID = new Guid("FAC164B8-0C72-4147-9EFA-4CE58C1A4DFD"),
                    Status = TransactionStatus.Accepted.ToString(),
                    StatusCode = TransactionCode.A_10000.ToString()
                },

            };
        }
        public void CreateTransaction(Transaction entity)
        {
            entity.TransactionID = new Guid();
            _transactions.Add(entity);
            //return entity;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public Transaction GetTransactionById(Guid transactionID)
        {
            return _transactions.Find(itm => itm.TransactionID == transactionID);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            var temp = _transactions.Find(itm => itm.TransactionID == transaction.TransactionID);
            try
            {
                _transactions.Remove(temp);
                _transactions.Add(transaction);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
