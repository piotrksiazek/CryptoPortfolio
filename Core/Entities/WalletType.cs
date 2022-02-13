using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class WalletType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}