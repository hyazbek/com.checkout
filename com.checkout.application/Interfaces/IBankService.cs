

using com.checkout.application.Models;

namespace com.checkout.application.Interfaces
{
    public interface IBankService
    {
        Task<BankResponse> ProcessTranaction(UnprocessedTransaction transaction);
    }
}
