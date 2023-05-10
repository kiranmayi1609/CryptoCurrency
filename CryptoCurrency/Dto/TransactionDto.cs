using CryptoCurrency.Models;

namespace CryptoCurrency.Dto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //Navigation property for user 
        public DateTime Date { get; set; }
    }

    public class updateTransaction
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
