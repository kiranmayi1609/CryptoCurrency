using CryptoCurrency.Models;

namespace CryptoCurrency.Interfaces
{
    public interface IUser
    {
        //ICollection<User> GetUsers();
        //User GetUser(int id);

        //void CreateUser(User userobj);
        //void UpdateUser(User userobj);
        //void DeleteUser(User userobj);

       
            ICollection<User> GetUsers();
            User GetUser(int id);
            void CreateUser(User userobj);
            void UpdateUser(User userobj);
            void DeleteUser(User userobj);
            User Authenticate(string email, string password);
            IEnumerable<Transaction> GetUserTransactions(int userId);





    }
}
