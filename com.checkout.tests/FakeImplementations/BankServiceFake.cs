using com.checkout.application.Helpers;
using com.checkout.application.Interfaces;
using com.checkout.application.Models;
using com.checkout.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.tests.FakeImplementations
{
    internal class BankServiceFake : IBankService
    {
        private readonly List<BankResponse> _responses;

        public BankServiceFake()
        {
            _responses = new List<BankResponse>()
            {
                new BankResponse(){
                    BankResponseID = new Guid(),
                    TransactionCode = TransactionCode.C_00001,
                    TransactionStatus = TransactionStatus.Created
                },
                new BankResponse(){
                    BankResponseID = new Guid(),
                    TransactionCode = TransactionCode.A_10000,
                    TransactionStatus = TransactionStatus.Accepted
                },
                new BankResponse(){
                    BankResponseID = new Guid(),
                    TransactionCode = TransactionCode.SD_20051,
                    TransactionStatus = TransactionStatus.FailedInsufficientFunds
                },
            };
        }

        public Task<BankResponse> ProcessTranaction(UnprocessedTransaction transaction, string url)
        {
            var response = new BankResponse() { BankResponseID = new Guid(), TransactionCode = TransactionCode.C_00001, TransactionStatus = TransactionStatus.Created };
            return Task.FromResult(response);
        }
    }
}
