
using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using com.checkout.data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace com.checkout.application.services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly EFRepository _contextService;
        public CurrencyService(EFRepository contextService)
        {
            _contextService = contextService;
        }

        public Currency GetCurrencyByID(int currencyId)
        {
            return _contextService.Currencies.ToList().Find(itm => itm.Id == currencyId);
        }
    }
}