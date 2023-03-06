using CryptoCurrency.Data;
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
            _dbContext.prices.Add(price);
            return true;
            
           
        }

        public bool DeletePrice(Price price)
        {
            throw new NotImplementedException();
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

        public bool UpdatePrice(Price price)
        {
            throw new NotImplementedException();
        }
    }
}
