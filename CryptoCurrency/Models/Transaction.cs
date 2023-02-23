namespace CryptoCurrency.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //Navigation property for user 
        public User User { get; set; }
        //Many to many relationship with Coin 
        public ICollection<TransactionCoin>TransactionCoins { get; set; }
        public DateTime Date { get; set; }
    }
}
