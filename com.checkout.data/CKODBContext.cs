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

            context.SaveChanges();
            #endregion
        }
    }
}