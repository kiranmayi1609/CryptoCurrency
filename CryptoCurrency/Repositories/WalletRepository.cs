using CryptoCurrency.Data;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;

namespace CryptoCurrency.Repositories
{
    public class WalletRepository:IWallet
    {

        private readonly CryptoDbContext _Context;

        public WalletRepository(CryptoDbContext Context)
        {
            _Context = Context;
        }
        public Wallet GetById(int id)
        {
            return _Context.wallets.FirstOrDefault(w => w.Id == id);
        }

       public IEnumerable<Wallet> GetAll()
       {
          return _Context.wallets.ToList();
       }

       public void Create(Wallet wallet)
       {
          _Context.wallets.Add(wallet);
          _Context.SaveChanges();
       }

        public void Update(Wallet wallet)
        {
         _Context.wallets.Update(wallet);
         _Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var wallet = GetById(id);
           _Context.wallets.Remove(wallet);
           _Context.SaveChanges();
        }
    }

    
}
