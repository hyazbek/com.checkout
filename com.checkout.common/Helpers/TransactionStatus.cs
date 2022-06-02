using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.common.Helpers
{

    public enum TransactionStatus
    {
        Created,
        Cancelled,
        Successful,
        FailedInsufficientFunds,
        FailedExpiredCard
    }

}
