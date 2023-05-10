using CryptoCurrency.Data;
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
            return _Context.users.FirstOrDefault(u => u.Id == id);
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
            var user = _Context.users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public IEnumerable<Transaction> GetUserTransactions(int userId)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Transaction> GetUserTransactions(int userId)
        //{
        //    //working with multiple tables using Linq join 

        //    var userList= from u in _Context.users
        //                  join ut in _Context.transactions on u.Id equals ut.UserId
        //                  join uw in _Context.wallets on u.Id equals uw.UserId
        //                  select new
        //                  {

        //                  }
        //    //return _Context.transactions
        //    //    .Include(t => t.User)
        //    //    .Include(t => t.TransactionCoins)
        //    //        .ThenInclude(tc => tc.Coin)
        //    //        .ThenInclude(c => c.Prices)
        //    //    .Where(t => t.UserId == userId)
        //    //    .ToList();
        //}





    }
}
