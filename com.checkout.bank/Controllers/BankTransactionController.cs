using com.checkout.application.Helpers;
using com.checkout.application.Models;
using com.checkout.bank.Model;
using Microsoft.AspNetCore.Mvc;
using BankResponse = com.checkout.bank.Model.BankResponse;

namespace com.checkout.bank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {

        [HttpPost]
        [Route("ProcessTransaction")]
        public IActionResult ProcessTransaction(UnprocessedTransaction transaction)
        {
            var moneyInAccount = 1000;
            var response = new BankResponse();

            if (transaction.Amount < moneyInAccount)
            {
                response.BankResponseID = Guid.NewGuid();
                response.TransactionStatus = TransactionStatus.Accepted;
                response.TransactionCode = TransactionCode.A_10000;
            }
            else
            {
                response.BankResponseID = Guid.NewGuid();
                response.TransactionStatus = TransactionStatus.FailedInsufficientFunds;
                response.TransactionCode = TransactionCode.SD_20051;
            }
            return Ok(response);
        }
    }
}
    