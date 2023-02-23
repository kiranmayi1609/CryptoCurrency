namespace CryptoCurrency.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int CoinId { get; set; }
        public decimal Value { get; set; }
        //Navigation property for coin 
        public Coin Coin { get; set; }
    }
}
