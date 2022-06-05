using com.checkout.data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace com.checkout.data
{
    public class CKODBContext : DbContext
    {
        public CKODBContext()
        {

        }
        public CKODBContext(DbContextOptions<CKODBContext> options)
            : base(options)
        {

        }

        public DbSet<CardDetails> Cards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            string conn = "Server=localhost\\SQLEXPRESS;Database=CKODB;Trusted_Connection = True; MultipleActiveResultSets = true";
            if (!options.IsConfigured)
            {
                options.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Merchant>().ToTable("Merchants");
            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<CardDetails>().ToTable("Cards");

            builder.Entity<CardDetails>()
            .Property(cd => cd.CardDetailsID)
            .ValueGeneratedOnAdd();

            #region Seed Data

            builder.Entity<Currency>().HasData(
                    new Currency { Id = 1, CurrencyCode = "QAR", CurrencyName = "Qatari Riyal" },
                    new Currency { Id = 2, CurrencyCode = "USD", CurrencyName = "US Dollar" },
                    new Currency { Id = 3, CurrencyCode = "GBP", CurrencyName = "British Pound" }
                );

            builder.Entity<Merchant>().HasData(
                    new Merchant { Id = Guid.Parse("1c4352e9-beb6-4c7f-8bfc-9263de60238b"), Name = "Github", Country = "USA" },
                    new Merchant { Id = Guid.NewGuid(), Name = "Amazon", Country = "USA" },
                    new Merchant { Id = Guid.NewGuid(), Name = "Farfetch", Country = "UK" }
                );
            builder.Entity<CardDetails>().HasData(
                    new CardDetails { CardDetailsID = 1, CardNumber = "123456789", Cvv = "111", ExpiryMonth = "11", ExpiryYear = "2024", HolderName = "Valid Card" }
                );
            builder.Entity<Transaction>().HasData(
                new Transaction() { TransactionID = Guid.Parse("ed9a5b76-b5cc-46f6-9372-7657a2812158"), MerchantID = Guid.Parse("1c4352e9-beb6-4c7f-8bfc-9263de60238b"), CardDetailsID = 1, Amount = new Decimal(322), CurrencyID = 1, StatusCode = "C_00001", Status = "Created" }
                );

            #endregion
        }


    }
}