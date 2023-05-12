namespace CryptoCurrency.Dto
{
    public class WalletDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public decimal Balance { get; set; }
    }

    public class updateWallet
    {
        public int UserId { get; set; }

        public decimal Balance { get; set; }

    }
}
