using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class TransactionWIthIDDto
    {
        [Required]
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public int CryptocurrencyId { get; set; }
    }
}
