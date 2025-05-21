namespace LotusPhoto.Api.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public string TransactionId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime PaidAt { get; set; }

        public Order Order { get; set; }
    }
}
