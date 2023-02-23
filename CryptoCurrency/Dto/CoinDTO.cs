namespace CryptoCurrency.Dto
{
    public class CoinDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal MarketCap { get; set; }
        public decimal Volume24h { get; set; }

        public decimal Change24h { get; set; }

    }
}
