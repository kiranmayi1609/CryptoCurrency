namespace CryptoCurrency.Models
{
    public class Coin
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string Symbol { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Volume24h { get; set; }

        public decimal Change24h { get; set; }

        //One to many relationship with price 
        public ICollection<Price > Prices { get; set; }

        //Many to many relationship with Trnasactions 
        public ICollection<TransactionCoin> TransactionCoins{ get; set; }

    }
}
