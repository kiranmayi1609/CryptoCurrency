using CryptoCurrency.Models;
using CryptoCurrency.Dto;

namespace CryptoCurrency.Interfaces
{
    public interface ITransaction
    {
        ICollection<Transaction>GetTransactions();
        Transaction GetTransaction(int id);
        ICollection<Coin> GetTransactionsByCoins(int transactionID);

        bool AddTransaction(Transaction transaction);
        bool UpdateTransaction(int id, updateTransaction update);
        bool Save();

        bool TransactionExists (int Id);

        void Delete(int id);
        //void AddTransaction(Transaction transaction);
        //void UpdateTransaction(int id, updateTransaction uTransaction);
    }
}
