namespace com.checkout.api.Helpers
{
    public class CardHelper
    {
        internal static string MaskCarNumber(string cardNumber)
        {
            int length = cardNumber.Length;

            var first = cardNumber.Substring(0,1);
            var last = cardNumber.Substring(length - 1);

            var remainingMask = new string('x',length - 2); // we will not mask 1st and last digits

            var maskedNumber = first + remainingMask + last;

            return maskedNumber;
        }
    }
}
