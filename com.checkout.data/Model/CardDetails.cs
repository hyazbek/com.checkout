namespace com.checkout.data.Model
{
    public class CardDetails
    {
        public int CardDetailsID { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string HolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
}
