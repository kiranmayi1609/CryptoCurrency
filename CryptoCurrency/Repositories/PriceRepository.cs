using CryptoCurrency.Data;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;

namespace CryptoCurrency.Repositories
{
    public class PriceRepository :IPrice
    {
        private readonly CryptoDbContext _dbContext;

        public PriceRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreatePrice(Price price)
        {

            var result = 0;
            Price p = _dbContext.prices.Where(c => c.Id == price.Id).FirstOrDefault();
            if (p!= null)
            {
                //p.Id = price.Id;
                p.CoinId = price.CoinId;
                p.Value = price.Value;
                result = _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.prices.Add(price);
                result = _dbContext.SaveChanges();

            }



            return result > 0;
            //_dbContext.prices.Add(price);
            //return true;
            
           
        }

        public bool DeletePrice(Price price)
        {
            _dbContext.Remove(price);
            return Save();
        }

        
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Coin> GetCoinsFromPrice(int priceId)
        {
            return _dbContext.coin
        .Where(c => c.Prices.Any(p => p.Id == priceId))
        .ToList();
        }

        public Price GetPrice(int priceId)
        {
           return  _dbContext.prices.Where(p=>p.Id == priceId).FirstOrDefault();
        }

       
        public ICollection<Price> GetPrices ()
        {
            return _dbContext.prices.ToList();
        }

        public ICollection<Price> GetPricesFromCoin(int coinId)
        {
            return _dbContext.prices  .Where(p => p.Coin.Id == coinId).ToList();
        }

        public bool PriceExists(int coinId)
        {
            return _dbContext.prices.Any(p => p.Coin.Id == coinId);
        }

       

        public bool UpdatePrice(int id, UpdatePrice updatePrice)
        {
           var price=_dbContext.prices.Find(id);

            if(price==null)
            {
                return false;
            }

            price.CoinId = updatePrice.CoinId;
            price.Value = updatePrice.Value;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
