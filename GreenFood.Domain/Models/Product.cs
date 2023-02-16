namespace GreenFood.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser Seller { get; set; } = null!;
        public string? Header { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public Guid TypeId { get; set; }
        public TypeOfProduct Type { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
