using CryptoCurrency.Models;

namespace CryptoCurrency.Interfaces
{
    public interface ICoin
    {
        ICollection <Coin> GetCoins();
        Coin GetCoin (int coinId);
        Coin GetCoin (string name);
       
        bool CoinExists(int coinId);
        bool CreateCoin( Coin coin);
        bool UpdateCoin(int transactionId,Coin coin);
        bool DeleteCoin(Coin coin);
        bool Save();
       
        
    }
}
