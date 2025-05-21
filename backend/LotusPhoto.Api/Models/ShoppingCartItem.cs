namespace LotusPhoto.Api.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
        public DateTime AddedAt { get; set; }

        public User User { get; set; }
        public Photo Photo { get; set; }
    }
}
