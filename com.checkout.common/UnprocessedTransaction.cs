namespace com.checkout.common
{
    public class UnprocessedTransaction
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string CardCvv { get; set; }
        public string HolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}
