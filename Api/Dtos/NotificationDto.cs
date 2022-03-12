namespace Api.Dtos
{
    public class NotificationDto
    {
        public decimal PricePoint { get; set; }
        public bool GreaterThanOrEqual { get; set; }
        public bool IsRecurring { get; set; }
        public int CryptocurrencyId { get; set; }
    }
}
