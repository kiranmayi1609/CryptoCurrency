using CryptoCurrency.Models;

namespace CryptoCurrency.Interfaces
{
    public interface ITransaction
    {
        ICollection<Transaction>GetTransactions();
        Transaction GetTransaction(int id);
        ICollection<Coin> GetTransactionsByCoins(int transactionID);
       
        bool TransactionExists (int Id);
    }
}
