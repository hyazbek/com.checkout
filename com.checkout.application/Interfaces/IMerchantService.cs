using com.checkout.data.Model;
using System;

namespace com.checkout.application.Interfaces
{
    public interface IMerchantService
    {
        Merchant GetMerchant(Guid merchantID);
        List<Merchant> GetMerchants();
    }
}