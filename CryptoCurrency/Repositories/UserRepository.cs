using CryptoCurrency.Data;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoCurrency.Repositories
{
    public class UserRepository:IUser
    {
        
            private readonly CryptoDbContext _Context;

            public UserRepository(CryptoDbContext Context)
            {
                _Context = Context;
            }


        //    public ICollection<User> GetUsers()
        //    {
        //        return _Context.users.ToList();
        //    }

        //    public User GetUser(int id)
        //    {
        //        return _Context.users.FirstOrDefault(u => u.Id == id);
        //    }

        //    public bool UserExists(int id)
        //    {
        //        return _Context.users.Any(u => u.Id == id);
        //    }

        //    public bool CreateUser(User userobj)
        //    {
        //        _Context.Add(userobj);
        //        return Save();
        //    }

        //    public bool UpdateUser(User userobj)
        //    {
        //        _Context.users.Update(userobj);
        //        return Save();
        //    }

        //    public bool DeleteUser(User userobj)
        //    {
        //        _Context.users.Remove(userobj);
        //        return Save();
        //    }

        //    public bool Save()
        //    {
        //        return _Context.SaveChanges() >= 0;
        //    }

        //public bool Authenticate(string Email, string Password)
        //{
        //    return _Context.users.Any(u => u.Email == Email && u.Password == Password);
        //}


        public ICollection<User> GetUsers()
        {
            return _Context.users.ToList();
        }

        public User GetUser(int id)
        {
            return _Context.users
                .Include(u=>u.Transactions)
                .Include(u=>u.Wallets)
                .FirstOrDefault(u => u.Id == id);
        }

        public void CreateUser(User userobj)
        {
            _Context.users.Add(userobj);
            _Context.SaveChanges();
        }

        public void UpdateUser(User userobj)
        {
            _Context.users.Update(userobj);
            _Context.SaveChanges();
        }

        public void DeleteUser(User userobj)
        {
            _Context.users.Remove(userobj);
            _Context.SaveChanges();
        }

        public User Authenticate(string email, string password)
        {
            //var user = _Context.users.Include(u=>u.Transactions)
            //                         .Include(u=>u.Wallets)
            //                         .FirstOrDefault(u => u.Email == email && u.Password == password);


            var user = _Context.users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return null;
            }

           

            return user;
        }

       

        //public IEnumerable<Transaction> GetUserTransactions(int userId)
        //{
        //    var query = from u in _Context.users
        //                join ut in _Context.transactions on u.Id equals ut.UserId
        //                join uw in _Context.wallets on u.Id equals uw.UserId
        //                where u.Id == userId
        //                select ut;

        //    return query.ToList();
        //}

        //public User GetUserWithTransactionsAndWallet(int userId)
        //{
        //    return _Context.users
        //        .Include(u=>u.Transactions)
        //        .Include(u=>u.Wallets)
        //        .FirstOrDefault(u=>u.Id== userId);
        //}

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            //throw new NotImplementedException();
            var walletTransactions=_Context.transactions.Where(u=>u.Id== userId).ToList();

            return walletTransactions;
        }

        public User GetUserByEmail(string email)
        {
            return _Context.users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByName(string firstName, string lastName)
        {
            return _Context.users.FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName);
        }

        public IEnumerable<User> GetUsersWithTransactionsAndWallets()
        {
            //throw new NotImplementedException();
            //return _Context.users.Include(u=>u.Transactions)
            //     .Include(u=>u.Wallets).ToList();
            //return _Context.users.
            //    Join(_Context.transactions,u=>u.Id,t=>t.UserId,(u,t)=> new { User =u, Transaction=t })
            //    .Join(_Context.wallets,ut=>ut.User.Id,w=>w.UserId,(ut,w)=> new {ut.User,ut.Transaction,Wallet=w})
            //    .GroupBy(utw=>utw.User.Id)
            //    .Select(utwGroup=>new User
            //    {
            //        Id=utwGroup.Key,
            //        FirstName=utwGroup.First().User.FirstName,
            //        LastName=utwGroup.First().User.LastName,
            //        Email=utwGroup.First().User.Email,
            //Transactions=utwGroup.Select(utw => utw.Transaction).ToList(),
            //Wallets=utwGroup.Select(utw => utw.Wallet).ToList()
            //Transactions=utwGroup.Select(utw => new Transaction
            //{
            //    Id=utw.Transaction.Id,
            //    Date=utw.Transaction.Date
            //}).ToList(),
            //Wallets=utwGroup.Select(utw => new Wallet
            //{
            //    Id=utw.Wallet.Id,
            //    Balance=utw.Wallet.Balance,
            //}).ToList(),
            //}).ToList();

            //      return _Context.users
            //.Join(_Context.transactions, u => u.Id, t => t.UserId, (u, t) => new { User = u, Transaction = t })
            //.Join(_Context.wallets, ut => ut.User.Id, w => w.UserId, (ut, w) => new { ut.User, ut.Transaction, Wallet = w })
            //.Select(utw => new User
            //{
            //    Id = utw.User.Id,
            //    FirstName = utw.User.FirstName,
            //    LastName = utw.User.LastName,
            //    Email = utw.User.Email,
            //    Transactions = new List<Transaction> { utw.Transaction },
            //    Wallets = new List<Wallet> { utw.Wallet }
            //})
            //.ToList();

            return _Context.users
       .Include(u => u.Transactions)
       .Include(u => u.Wallets)
       .ToList();

        }
    }
}
