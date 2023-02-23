namespace CryptoCurrency.Models
{
    public class Wallet
    {
        public int Id { get; set; } 
        public int UserId { get; set; }

        public decimal Balance { get; set; }    
        //Navigation property for user 
        public User User { get; set; }
    }
}
