namespace LotusPhoto.Api.Models
{
    // TODO : More details will be added in the next phase/version
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public decimal PricePaid { get; set; }

        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }

        public string DownloadLink { get; set; }
        public DateTime? DownloadLinkExpiresAt { get; set; }

        public User User { get; set; }
        public Photo Photo { get; set; }
    }
}
