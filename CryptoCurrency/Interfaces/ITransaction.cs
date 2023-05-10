using CryptoCurrency.Models;
using CryptoCurrency.Dto;

namespace CryptoCurrency.Interfaces
{
    public interface ITransaction
    {
        ICollection<Transaction>GetTransactions();
        Transaction GetTransaction(int id);
        ICollection<Coin> GetTransactionsByCoins(int transactionID);
       
        bool TransactionExists (int Id);
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(int id, updateTransaction uTransaction);
    }
}
