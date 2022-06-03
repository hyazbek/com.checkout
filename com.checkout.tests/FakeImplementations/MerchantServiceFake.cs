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
    internal class MerchantServiceFake : IMerchantService
    {
        private readonly List<Merchant> _merchants;

        public MerchantServiceFake()
        {
            _merchants = new List<Merchant>()
            {
                new Merchant()
                {
                    Country = "USA",
                    Id = new Guid(),
                    Name = "Amazon"
                }
            };
        }

        public Merchant GetMerchant(Guid merchantID)
        {
            return _merchants.Find(itm => itm.Id == merchantID);
        }

        public List<Merchant> GetMerchants()
        {
            return _merchants;
        }
    }
}
