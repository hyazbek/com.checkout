using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.application.Helpers
{

    public enum TransactionStatus
    {
        Created,
        Accepted,
        Declined,
        FailedInsufficientFunds,
        FailedExpiredCard
    }

    // getting codes from CKO site.
    // Letter_Status Code ( A_1000 = Accepted 1000)
    public enum TransactionCode
    {
        C_00001,    // Created 
        A_10000,    // Accepted
        SD_20000,   // Soft Decline Declined
        SD_20051,   // Soft Decline Insufficient Funds
        SD_20054    // Soft Decline Expired Card
    }
}
