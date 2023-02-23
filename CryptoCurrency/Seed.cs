using CryptoCurrency.Models;
using CryptoCurrency.Data;

namespace CryptoCurrency
{
    public class Seed
    {
        private readonly CryptoDbContext cryptoDbContext;
        public Seed(CryptoDbContext cryptoDbContext)
        {
            this.cryptoDbContext = cryptoDbContext;
        }
        //Coins seed data 
        public void SeedDataContext()
        {
            if (!cryptoDbContext.coin.Any())
            {
                var coins = new List<Coin>
                {
                    new Coin { Name = "Bitcoin", Symbol = "BTC", MarketCap = 1000000000, Volume24h = 100000000 ,Change24h = 1.5m },
                    new Coin { Name = "Ethereum", Symbol = "ETH", MarketCap = 500000000, Volume24h = 50000000, Change24h = 0.5m },
                    new Coin { Name = "Binance Coin", Symbol = "BNB", MarketCap = 100000000, Volume24h = 10000000, Change24h = -0.5m },

                };

                cryptoDbContext.coin.AddRange(coins);
                cryptoDbContext.SaveChanges();
            }

            //Prices seed data 
            if (!cryptoDbContext.prices.Any())
            {
                var prices = new List<Price>
                {
                   new Price { CoinId = 1, Value = 55000 },
                   new Price { CoinId = 1, Value = 56000 },
                    new Price { CoinId = 1, Value = 57000 },
                   new Price { CoinId = 2, Value = 2000 },
                    new Price { CoinId = 2, Value = 2100 },
                   new Price { CoinId = 2, Value = 2200 },
        
                };

                cryptoDbContext.prices.AddRange(prices);
                cryptoDbContext.SaveChanges();
            }
            //users seed data 
            if (!cryptoDbContext.users.Any())
            {
                 var users = new List<User>
                 {
                  new User { FirstName = "Kt", LastName = "Tummala", Email = "kk@example.com", Password = "password123" },
                  new User { FirstName = "ZA", LastName = "Ali", Email = "Za@example.com", Password = "password456" },
                  
                 };

                cryptoDbContext.users.AddRange(users);
                cryptoDbContext.SaveChanges();
            }
            //Wallets seed data 
            if (!cryptoDbContext.wallets.Any())
            {
                var wallets = new List<Wallet>
                {
                   new Wallet {  UserId = 1,Balance=1000 },
                   new Wallet {  UserId = 2,Balance=500 },
                    new Wallet { UserId = 3,Balance=200 },
     
                };

                cryptoDbContext.wallets.AddRange(wallets);
                cryptoDbContext.SaveChanges();
            }


            //Transaction seed data 

                if (!cryptoDbContext.transactions.Any())
                {
                    // Retrieve some users and coins to use in the transactions
                    var users = cryptoDbContext.users.Take(3).ToList();
                    var coins = cryptoDbContext.coin.Take(3).ToList();

                    // Create some transaction coins to use in the transactions
                    var transactionCoins = new List<TransactionCoin>
                    {
                     new TransactionCoin { Coin = coins[0], Amount = 2.5m },
                     new TransactionCoin { Coin = coins[1], Amount = 1.5m },
                     new TransactionCoin { Coin = coins[2], Amount = 3m }
                    };

                    var transactions = new List<Transaction>
                    {
                     new Transaction { UserId = users[0].Id, User = users[0], TransactionCoins = transactionCoins, Date = new DateTime(2022, 2, 15) },
                     new Transaction { UserId = users[1].Id, User = users[1], TransactionCoins = transactionCoins, Date = new DateTime(2022, 2, 14) },
                    new Transaction { UserId = users[2].Id, User = users[2], TransactionCoins = transactionCoins, Date = new DateTime(2022, 2, 13) }
                    };

                    cryptoDbContext.transactions.AddRange(transactions);
                    cryptoDbContext.SaveChanges();
                }
        }
    }


}






