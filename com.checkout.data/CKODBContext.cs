using com.checkout.data.Model;
using Microsoft.EntityFrameworkCore;

namespace com.checkout.data
{
    public class CKODBContext: DbContext
    {
        public CKODBContext(DbContextOptions<CKODBContext> options)
            : base(options)
        {
        }

        public DbSet<CardDetails> Cards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        public void LoadTestData(CKODBContext context)
        {
            #region Cards Data
            var card1 = new CardDetails
            {
                CardDetailsID = 1,
                CardNumber = "4242424242424242",
                Cvv = "100",
                HolderName = "user1",
                ExpiryMonth = "12",
                ExpiryYear = "2030"
            };
            context.Cards.Add(card1);
            var card2 = new CardDetails
            {
                CardDetailsID = 2,
                CardNumber = "4543474002249996",
                Cvv = "200",
                HolderName = "user2",
                ExpiryMonth = "12",
                ExpiryYear = "2025"
            };
            context.Cards.Add(card2);
            var card3 = new CardDetails
            {
                CardDetailsID = 3,
                CardNumber = "4002931234567895",
                Cvv = "300",
                HolderName = "user3",
                ExpiryMonth = "12",
                ExpiryYear = "2024"
            };
            context.Cards.Add(card3);
            #endregion

            #region Merchant Data

            var merchant1 = new Merchant
            {
                Id = 1,
                Name = "github",
                Country = "USA"
            };
            context.Merchants.Add(merchant1);

            var merchant2 = new Merchant
            {
                Id = 2,
                Name = "Amazon",
                Country = "USA"
            };
            context.Merchants.Add(merchant2);

            var merchant3 = new Merchant
            {
                Id = 3,
                Name = "Checkout",
                Country = "UK"
            };
            context.Merchants.Add(merchant3);
            context.SaveChanges();
            #endregion

            #region Currency Data

            var currency1 = new Currency
            {
                Id = 1,
                CurrencyCode="USD",
                CurrencyName="US Dollar"
            };
            context.Currencies.Add(currency1);

            var currency2 = new Currency
            {
                Id = 2,
                CurrencyCode = "GBP",
                CurrencyName = "British Pound"
            };
            context.Currencies.Add(currency2);

            context.SaveChanges();
            #endregion
        }
    }
}