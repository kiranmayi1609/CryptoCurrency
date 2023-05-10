using CryptoCurrency.Data;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using System.Linq;

namespace CryptoCurrency.Repositories
{
    public class CoinRepository:ICoin
    {
        private readonly CryptoDbContext _Context;
        public CoinRepository(CryptoDbContext Context)
        {
            _Context = Context;
        }

        public bool CoinExists(int coinId)
        {
            return _Context.coin.Any(c=>c.Id == coinId);
        }

        public bool CreateCoin( Coin createCoin)
        {
            //var transactionCoinEntity=_Context. transactions.Where(a=> a.Id == transactionId).FirstOrDefault();
            //var transactionCoin = new TransactionCoin()
            //{
            //    Transaction = transactionCoinEntity,
            //    Coin = coin,

            //};
                _Context.coin.Add(createCoin);
                var result = _Context.SaveChanges();

                return result > 0;

        }

        public bool DeleteCoin(Coin coin)
        {
           _Context.Remove(coin);
            return Save();
        }

        public Coin GetCoin(int id)
        {
            return _Context.coin.Where(c=>c.Id==id).FirstOrDefault();
        }

        public Coin GetCoin(string name)
        {
           return _Context.coin.Where(c=>c.Name==name).FirstOrDefault();    
        }

       
        public ICollection<Coin>GetCoins()
        {
            return _Context.coin.OrderBy(c=> c.Id).ToList();
        }

        public bool Save()
        {
           var  saved =_Context.SaveChanges();
            return saved>0? true: false;
        }

        public bool UpdateCoin(int transactionId, Coin coin)
        {
            _Context.Update(coin);
            return Save();
        }
    }
}
