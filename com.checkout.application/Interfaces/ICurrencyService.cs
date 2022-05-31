
using com.checkout.data.Model;
using System.Collections.Generic;

namespace com.checkout.application.Interfaces
{
    public interface ICurrencyService
    {
        Currency GetCurrencyByCode(string currency);
    }
}