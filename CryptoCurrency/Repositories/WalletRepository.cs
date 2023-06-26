using CryptoCurrency.Data;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;

namespace CryptoCurrency.Repositories
{
    public class WalletRepository : IWallet
    {

        private readonly CryptoDbContext _Context;

        public WalletRepository(CryptoDbContext Context)
        {
            _Context = Context;
        }
        public bool walletExists(int walletId)
        {
            return _Context.wallets.Any(c => c.Id == walletId);
        }
        public Wallet GetById(int id)
        {
            return _Context.wallets.FirstOrDefault(w => w.Id == id);
        }

        public IEnumerable<Wallet> GetAll()
        {
            return _Context.wallets.ToList();
        }

        //public void Create(Wallet wallet)
        //{
        //    _Context.wallets.Add(wallet);
        //    _Context.SaveChanges();
        //}

         public bool CreateWallet(Wallet createWallet)
        {
            //var transactionCoinEntity=_Context. transactions.Where(a=> a.Id == transactionId).FirstOrDefault();
            //var transactionCoin = new TransactionCoin()
            //{
            //    Transaction = transactionCoinEntity,
            //    Coin = coin,

            //};
            var result = 0;
            Wallet s = _Context.wallets.Where(c => c.Id == createWallet.Id).FirstOrDefault();
            if (s != null)
            {
                s.UserId = createWallet.UserId;
                s.Balance = createWallet.Balance;
                result = _Context.SaveChanges();
            }
            else
            {
                _Context.wallets.Add(createWallet);
                result = _Context.SaveChanges();

            }



            return result > 0;

        }

        //public void Update(Wallet wallet)
        //{

        //    var wall = _Context.wallets.FirstOrDefault(w => w.Id == wallet.Id);
        //    if (wallet != null)
        //    {
        //        wall.UserId = wallet.UserId;


        //        wall.Balance = wallet.Balance;

        //        _Context.SaveChanges();

        //    }


            //_Context.wallets.Update(wallet);
            //_Context.SaveChanges();
        //}

        public void Delete(int id)
        {
            var wallet = GetById(id);
            _Context.wallets.Remove(wallet);
            _Context.SaveChanges();
        }

        //public void Update(int id,updateWallet uWallet)
        //{

        //    var wallet = _Context.wallets.FirstOrDefault(w => w.Id == id);

        //    if (wallet != null)
        //    {
        //        wallet.UserId = uWallet.UserId;
        //        wallet.Balance = uWallet.Balance;
        //        _Context.SaveChanges();
        //    }
        //    _Context.Entry(uWallet).State=Microsoft.EntityFrameworkCore.EntityState.Modified;

        //    try
        //    {
        //        _Context.SaveChanges();
        //    }
        //    catch(Exception E)
        //    {
        //        Console.WriteLine(E.Message);
        //    }
        //}


        //public void Update(Wallet wallet)
        //{
        //    _Context.SaveChanges();
        //}


        //public void Update(updateWallet uWallet)
        //{
        //    _Context.wallets.Update(uWallet);
        //    _Context.SaveChanges();
        //}

        public bool UpdateWallet(int id, updateWallet update)
        {
            var wallet = _Context.wallets.Find(id);
            if (wallet != null)
            {
                wallet.UserId = update.UserId;
                wallet.Balance = update.Balance;
                _Context.SaveChanges();

            }
            return true;
        }

        public bool Save()
        {
            var saved = _Context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
