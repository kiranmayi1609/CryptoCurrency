using CryptoCurrency.Models;
using CryptoCurrency.Dto;

namespace CryptoCurrency.Interfaces
{
    public interface IUser
    {
        
            ICollection<User> GetUsers();
            User GetUser(int id);
            void CreateUser(User userobj);
            void UpdateUser(User userobj);
            void DeleteUser(User userobj);
            User Authenticate(string email, string password);
            IEnumerable<Transaction> GetUserTransactions(int userId);
            bool IsPasswordValid(string password);
        bool IsEmailValid(string email);

        //User GetUserWithTransactionsAndWallet(int userId);
        IEnumerable<User> GetUsersWithTransactionsAndWallets();

        User GetUserByEmail(string email);
        User GetUserByName(string firstName,string lastName );

           //UserDto1 MapUserToDto(User user);






    }
}
