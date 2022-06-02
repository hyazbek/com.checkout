using com.checkout.bank.Model;
using com.checkout.common;
using com.checkout.common.Helpers;
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
                response.BankResponseID = new Guid();
                response.Status = TransactionStatus.Successful;
            }
            else
            {
                response.BankResponseID = new Guid();
                response.Status = TransactionStatus.FailedInsufficientFunds;
            }
            return Ok(response);
        }
    }
}
