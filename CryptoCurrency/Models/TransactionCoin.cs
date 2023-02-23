namespace CryptoCurrency.Models
{
    //Intermediate relationship from many to many relationship 
    public class TransactionCoin
    {
        public int TransactionId { get; set; }
        public int CoinID { get; set; } 
        //Navigation properties for coin and Transaction 
        public Transaction Transaction { get; set; }
        public Coin Coin { get; set; }
        public decimal Amount { get; set; }
    }
}
