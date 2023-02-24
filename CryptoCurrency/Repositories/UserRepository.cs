﻿using CryptoCurrency.Data;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;

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
                return _Context.users.FirstOrDefault(u => u.Id == id);
            }

            public bool UserExists(int id)
            {
                return _Context.users.Any(u => u.Id == id);
            }

            public bool CreateUser(User userobj)
            {
                _Context.Add(userobj);
                return Save();
            }

            public bool UpdateUser(User userobj)
            {
                _Context.users.Update(userobj);
                return Save();
            }

            public bool DeleteUser(User userobj)
            {
                _Context.users.Remove(userobj);
                return Save();
            }

            public bool Save()
            {
                return _Context.SaveChanges() >= 0;
            }

            public bool Authenticate(string Email, string Password)
            {
                return _Context.users.Any(u => u.Email == Email && u.Password == Password);
            }

            
        

    }
}
