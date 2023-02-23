using CryptoCurrency.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoCurrency.Data
{
    //CryptoDbcontext class is a subclass of Dbcontext and serves as a representation of the Database context 
    public class CryptoDbContext :DbContext
    {
        //The 'DbContextOptions<CryptoDbContext>' parameter in the constructor is used configure the database connection 
        public CryptoDbContext(DbContextOptions<CryptoDbContext>options):base(options)
        {

        }
        //Dbsets represent the entities in the database 
        public DbSet<User>users { get; set; }
        public DbSet<Price>prices { get; set; }
        public DbSet<Transaction>transactions { get; set; }
        public DbSet<Coin>coin { get; set; }
        public DbSet<Wallet>wallets { get; set; }
        public DbSet<TransactionCoin>transactionCoins { get; set; }

        //On model creating method is used to confgure the relation ship between entities in the databse 
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base .OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coin>(coin =>
            {
                coin.Property(c => c.Change24h).HasColumnType("decimal(18,6)");
                coin.Property(c => c.MarketCap).HasColumnType("decimal(18,6)");
                coin.Property(c => c.Volume24h).HasColumnType("decimal(18, 6)");
            });
            modelBuilder.Entity<Price>(price =>
            {
                price.Property(p => p.Value).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<TransactionCoin>(transactionCoin =>
            {
                transactionCoin.Property(tc => tc.Amount).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<Wallet>(wallet =>
            {
                wallet.Property(w => w.Balance).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<TransactionCoin>()
                .HasKey(tc => new { tc.CoinID, tc.TransactionId });

            modelBuilder.Entity<TransactionCoin>()
                .HasOne(tc => tc.Transaction)
                .WithMany(t => t.TransactionCoins)
                .HasForeignKey(tc => tc.TransactionId);

            modelBuilder.Entity<TransactionCoin>()
                .HasOne(tc => tc.Coin)
                .WithMany(c => c.TransactionCoins)
                .HasForeignKey(tc => tc.CoinID);
            

           
        }



        


    }
}
