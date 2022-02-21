using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class TransactionDto
    {
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int CryptocurrencyId { get; set; }
    }
}
