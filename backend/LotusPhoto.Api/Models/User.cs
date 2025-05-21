namespace LotusPhoto.Api.Models
{
    // TODO : More details will be added later
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public DateTime RegisteredAt { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
