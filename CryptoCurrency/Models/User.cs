namespace CryptoCurrency.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string Username { get; set; }
         public string Password { get; set; }

        //one to many relationship with Transaction 
        public ICollection<Transaction> Transactions { get; set; }
        //one to many relationship 
        public ICollection<Wallet> Wallets { get; set; }    
    }
}
