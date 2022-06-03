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
    internal class CurrencyServiceFake : ICurrencyService
    {
        private readonly List<Currency> _currencies;

        public CurrencyServiceFake()
        {
            _currencies = new List<Currency>()
            {
                new Currency()
                {
                    CurrencyCode = "QAR",
                    CurrencyName = "Qatari Riyal",
                    Id = 1
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    CurrencyName = "US Dollar",
                    Id = 2
                },
                new Currency()
                {
                    CurrencyCode = "LBP",
                    CurrencyName = "Lebanese Pound",
                    Id = 3
                }
            };
        }
       
        public Currency GetCurrencyByID(int currency)
        {
            return _currencies.Find(itm => itm.Id == currency);
        }


    }
}
