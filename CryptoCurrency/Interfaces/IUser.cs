using CryptoCurrency.Models;

namespace CryptoCurrency.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
        bool CreateUser(User userobj);
        bool UpdateUser(User userobj);
        bool DeleteUser(User userobj);
        bool Save();
        bool Authenticate(string Email, string Password);
    }
}
