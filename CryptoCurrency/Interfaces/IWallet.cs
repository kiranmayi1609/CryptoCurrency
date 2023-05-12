using CryptoCurrency.Models;
using CryptoCurrency.Dto;

namespace CryptoCurrency.Interfaces
{
    public interface IWallet
    {
        
            // Get a wallet by its ID
            Wallet GetById(int id);

            // Get all wallets
            IEnumerable<Wallet> GetAll();

            // Create a new wallet
            void Create(Wallet wallet);

            // Update an existing wallet
            void Update( int id,updateWallet uWallet);

            // Delete a wallet by its ID
            void Delete(int id);
        



    }
}
