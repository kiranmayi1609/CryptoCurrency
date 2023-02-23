using CryptoCurrency.Data;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;

namespace CryptoCurrency.Repositories
{
    public class TransactionRepository : ITransaction
    {
        private readonly CryptoDbContext _dbContext;
        public TransactionRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Transaction GetTransaction(int id)
        {
            return _dbContext.transactions.Where(t => t.Id == id).FirstOrDefault();
        }

      
        public ICollection<Transaction> GetTransactions()
        {
            return _dbContext.transactions.ToList();
        }

        public ICollection<Coin> GetTransactionsByCoins(int transactionID)
        {
           return _dbContext.transactionCoins.Where(tc=>tc.TransactionId==transactionID).Select(c=>c.Coin).ToList();
        }

        public bool TransactionExists(int Id)
        {
            return _dbContext.transactions.Any(x => x.Id == Id);
        }
    }
}
