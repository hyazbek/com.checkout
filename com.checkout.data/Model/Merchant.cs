namespace com.checkout.data.Model
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<Transaction> PaymentRequests { get; set; }
    }
}
