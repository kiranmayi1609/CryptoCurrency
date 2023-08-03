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
            
            var result = 0;
            Coin s=_Context.coin.Where(c=>c.Id==createCoin.Id).FirstOrDefault();
            if(s!=null)
            {
                s.Name = createCoin.Name;
                s.MarketCap = createCoin.MarketCap;
                s.Change24h = createCoin.Change24h;
                s.Volume24h = createCoin.Volume24h;
                s.Symbol=createCoin.Symbol;

                result = _Context.SaveChanges();
            }
            else
            {
                _Context.coin.Add(createCoin);
                 result = _Context.SaveChanges();

            }
               
               

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
