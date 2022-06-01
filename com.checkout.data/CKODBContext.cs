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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Merchant>().ToTable("Merchants");
            builder.Entity<Transaction>().ToTable("Transactions");

            builder.Entity<CardDetails>()
            .Property(cd => cd.CardDetailsID)
            .ValueGeneratedOnAdd();

            #region Seed Data

            builder.Entity<Currency>().HasData(
                    new Currency { protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Merchant>().ToTable("Merchants");
            builder.Entity<Bank>().ToTable("Banks");
            builder.Entity<Transaction>().ToTable("Transactions");

            builder.Entity<CardDetails>()
            .Property(cd => cd.CardDetailsID)
            .ValueGeneratedOnAdd();

            #region Seed Data

            builder.Entity<Currency>().HasData(
                    new Currency { Id = 1, CurrencyCode= "QAR", CurrencyName = "Qatari Riyal" },
                    new Currency { Id = 2, CurrencyCode = "USD", CurrencyName = "US Dollar"},
                    new Currency { Id = 3, CurrencyCode = "GBP" , CurrencyName = "British Pound" }
                );

            builder.Entity<Merchant>().HasData(
                    new Merchant { Id = Guid.NewGuid(), Name = "Github", Country="USA"},
                    new Merchant { Id = Guid.NewGuid(), Name = "Amazon", Country = "USA" },
                    new Merchant { Id = Guid.NewGuid(), Name = "Farfetch", Country = "UK" },
                );

            builder.Entity<Bank>().HasData(
                    new Bank { BankID  = 1, BankName = "MockBank", BankURL = "https://checkoutmockbank.azurewebsites.net/" },
                    new Bank { BankID = 2, BankName = "LocalTestBank", BankURL = "http://localhost:62268/" }
                );

            #endregion
        } = 1, Name = "ZAR" },
                    new Currency { CurrencyId = 2, Name = "USD" },
                    new Currency { CurrencyId = 3, Name = "GBP" }
                );

            builder.Entity<Merchant>().HasData(
                    new Merchant { MerchantID = Guid.NewGuid(), Name = "Test Merchant 1", Active = true, Description = "Testing Description 1" },
                    new Merchant { MerchantID = Guid.NewGuid(), Name = "Test Merchant 2", Active = true, Description = "Testing Description 2" },
                    new Merchant { MerchantID = Guid.NewGuid(), Name = "Test Merchant 3", Active = true, Description = "Testing Description 3" }
                );

            builder.Entity<Bank>().HasData(
                    new Bank { BankID = 1, BankName = "MockBank", BankURL = "https://checkoutmockbank.azurewebsites.net/" },
                    new Bank { BankID = 2, BankName = "LocalTestBank", BankURL = "http://localhost:62268/" }
                );

            #endregion
        }

        //public void LoadTestData(CKODBContext context)
        //{
        //    #region Cards Data
        //    var card1 = new CardDetails
        //    {
        //        CardDetailsID = 1,
        //        CardNumber = "4242424242424242",
        //        Cvv = "100",
        //        HolderName = "user1",
        //        ExpiryMonth = "12",
        //        ExpiryYear = "2030"
        //    };
        //    context.Cards.Add(card1);
        //    var card2 = new CardDetails
        //    {
        //        CardDetailsID = 2,
        //        CardNumber = "4543474002249996",
        //        Cvv = "200",
        //        HolderName = "user2",
        //        ExpiryMonth = "12",
        //        ExpiryYear = "2025"
        //    };
        //    context.Cards.Add(card2);
        //    var card3 = new CardDetails
        //    {
        //        CardDetailsID = 3,
        //        CardNumber = "4002931234567895",
        //        Cvv = "300",
        //        HolderName = "user3",
        //        ExpiryMonth = "12",
        //        ExpiryYear = "2024"
        //    };
        //    context.Cards.Add(card3);
        //    #endregion

        //    #region Merchant Data

        //    var merchant1 = new Merchant
        //    {
        //        Id = 1,
        //        Name = "github",
        //        Country = "USA"
        //    };
        //    context.Merchants.Add(merchant1);

        //    var merchant2 = new Merchant
        //    {
        //        Id = 2,
        //        Name = "Amazon",
        //        Country = "USA"
        //    };
        //    context.Merchants.Add(merchant2);

        //    var merchant3 = new Merchant
        //    {
        //        Id = 3,
        //        Name = "Checkout",
        //        Country = "UK"
        //    };
        //    context.Merchants.Add(merchant3);
        //    context.SaveChanges();
        //    #endregion

        //    #region Currency Data

        //    var currency1 = new Currency
        //    {
        //        Id = 1,
        //        CurrencyCode="USD",
        //        CurrencyName="US Dollar"
        //    };
        //    context.Currencies.Add(currency1);

        //    var currency2 = new Currency
        //    {
        //        Id = 2,
        //        CurrencyCode = "GBP",
        //        CurrencyName = "British Pound"
        //    };
        //    context.Currencies.Add(currency2);

        //    context.SaveChanges();
        //    #endregion
        //}
    }
}