namespace LotusPhoto.Api.Models
{
    // TODO : More details will be added in the next phase/version
    public class Order
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}
