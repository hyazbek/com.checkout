using com.checkout.data.Model;
using Microsoft.EntityFrameworkCore;

namespace com.checkout.data
{
    public class CKODBContext: DbContext
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
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
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
                    new Merchant { Id = Guid.NewGuid(), Name = "Github", Country = "USA" },
                    new Merchant { Id = Guid.NewGuid(), Name = "Amazon", Country = "USA" },
                    new Merchant { Id = Guid.NewGuid(), Name = "Farfetch", Country = "UK" }
                );

            #endregion
        }

        
    }
}