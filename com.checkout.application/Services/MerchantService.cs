using com.checkout.application.Interfaces;
using com.checkout.data.Model;
using com.checkout.data.Repository;
using System;
using System.Linq;

namespace com.checkout.application.services
{
    public class MerchantService : IMerchantService
    {
        private readonly EFRepository _contextService;

        public MerchantService(EFRepository contextService)
        {
            _contextService = contextService;
        }

        /// <summary>
        /// Search for the merchant by merchantID
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public Merchant GetMerchant(int merchantID)
        {
            return _contextService.Merchants.ToList().Find(itm => itm.Id == merchantID);
        }
    }
}