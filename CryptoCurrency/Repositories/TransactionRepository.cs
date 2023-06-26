using CryptoCurrency.Data;
using CryptoCurrency.Dto;
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

        public bool TransactionExists(int transId)
        {
            return _dbContext.transactions.Any(c => c.Id == transId);
        }

        public bool AddTransaction(Transaction transaction)
        {
            //var transactionEntity=_dbContext. transactions.Where(a=> a.Id==transaction.Id).FirstOrDefault();
            //var transactionCoin = new TransactionCoin()
            //{
            //    Transaction = transactionCoinEntity,
            //    Coin = coin,

            //};
            var result = 0;
            Transaction s = _dbContext.transactions.Where(c => c.Id == transaction.Id).FirstOrDefault();
            if (s != null)
            {
                s.UserId= transaction.UserId;
                s.Date= transaction.Date;

                result = _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.transactions.Add(transaction);
                result = _dbContext.SaveChanges();

            }



            return result > 0;

        }

        //public void AddTransaction(Transaction transaction)
        //{
        //    _dbContext.transactions.Add(transaction);
        //    _dbContext.SaveChanges();
        //}

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

      

        //public void UpdateTransaction(Transaction transaction)
        //{
        //    _dbContext.transactions.Update(transaction);
        //    _dbContext.SaveChanges();
        //}

        //public void UpdateTransaction(int id,  updateTransaction uTransaction)
        //{
        //    var transaction = _dbContext.transactions.FirstOrDefault(x => x.Id == id);
        //    if(transaction != null)
        //    {
        //        transaction.UserId = uTransaction.UserId;
        //        transaction.Date = uTransaction.Date;
        //        _dbContext.SaveChanges();

        //    }
            


        //}

        public bool UpdateTransaction(int id, updateTransaction update)
        {
            var transaction = _dbContext.transactions.Find(id);

            //var coins = _dbContext.coin.Find(id);
            if (transaction != null)
            {
                transaction.UserId = update.UserId;
                transaction.Date = update.Date;
                _dbContext.SaveChanges();

            }
            return true;

        }

        public bool Save()
        {
        var saved = _dbContext.SaveChanges();
        return saved > 0 ? true : false;
        }

        public void Delete(int id)
        {
            var transaction= GetTransaction(id);
            _dbContext.transactions.Remove(transaction);
            _dbContext.SaveChanges();
        }
    }
}
