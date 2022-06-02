

using com.checkout.common;

namespace com.checkout.application.Interfaces
{
    public interface IBankService
    {
        Task<BankResponse> ProcessTranaction(UnprocessedTransaction transaction);
    }
}
