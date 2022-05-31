using com.checkout.data.Model;
using System;

namespace com.checkout.application
{
    public interface IMerchantService
    {
        Merchant GetMerchant(Guid merchantID);
    }
}