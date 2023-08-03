using CryptoCurrency.Data;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CryptoCurrency.Repositories
{
    public class UserRepository:IUser
    {
        
            private readonly CryptoDbContext _Context;

            public UserRepository(CryptoDbContext Context)
            {
                _Context = Context;
            }


       


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
            // Check if the password meets the requirements
            if (!IsPasswordValid(password))
            {
                return null;
            }


            var user = _Context.users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return null;
            }

           

            return user;
        }

      

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

            return _Context.users
       .Include(u => u.Transactions)
       .Include(u => u.Wallets)
       .ToList();

        }

        public  bool IsPasswordValid(string password)
        {
            // Define your password requirements here
            int minimumLength = 5;
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;

            if (string.IsNullOrEmpty(password) || password.Length < minimumLength)
            {
                return false;
            }

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            return hasUppercase && hasLowercase && hasDigit;
        

        }

        public bool IsEmailValid(string email)
        {
            // You can use a regular expression to validate the email format
            // Here's a simple example, but you can adjust the regex pattern as needed
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
