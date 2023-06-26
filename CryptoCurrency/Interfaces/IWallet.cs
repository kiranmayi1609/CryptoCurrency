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
        //void Create(Wallet wallet);
       bool walletExists(int walletId);

        bool CreateWallet(Wallet wall);

        // Update an existing wallet
        //void Update( int id,updateWallet uWallet);

        //void Update( Wallet wallet );

        bool UpdateWallet(int id, updateWallet update);

        // Delete a wallet by its ID
        void Delete(int id);

        bool Save();




    }
}
