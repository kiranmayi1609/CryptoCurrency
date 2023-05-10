using CryptoCurrency.Data;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using System.Linq;
using CryptoCurrency.Dto;

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

        //public bool UpdateCoin(int Id, CoinDto coinDto)
        //{
        //    var coin=_Context.C
        //    _Context.Update(coin);
        //    return Save();
        //}

        public bool UpdateCoin(int id, CoinDTO coinDto)
        {
            var coin = _Context.coin.FirstOrDefault(x => x.Id == id);

            if (coin == null)
            {
                return false;
            }

            coin.Name = coinDto.Name;
            coin.MarketCap= coinDto.MarketCap;

            _Context.coin.Update(coin);
            return _Context.SaveChanges() > 0;
        }

        public bool UpdateCoin(int id, updateCoin update)
        {
            var coins = _Context.coin.Find(id);
            if(coins!=null)
            {
                coins.Name = update.Name;
                coins.Symbol = update.Symbol;
                coins.MarketCap= update.MarketCap;
                coins.Volume24h= update.Volume24h;
                coins.Change24h= update.Change24h;

                _Context.SaveChanges();
               
            }
            return true;

        }
    }
    
}
