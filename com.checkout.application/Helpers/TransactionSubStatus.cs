using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.checkout.application.Helpers
{

    public enum TransactionSubStatus
    {
        DeclinedDonothonour,
        InvalidPayment,
        InvalidCardNumber,
        InsufficientFunds,
        BadTrackData,
        RestrictedCard,
        SecurityViolation,
        ResponseTimeout,
        CardNot3DSecureEnabled,
        UnableToSpecifyIfCardIsSecureEnabled,
        SecureSystemMalfunction3DSecure,
        SecureAuthenticationRequired3DSecure,
        ExpiredCard,
        PaymentBlockedDueToRisk,
        CardNumberBlacklisted,
        StopPaymenttThisAuth,
        StopPaymentAll,
        Successful
    }

}
