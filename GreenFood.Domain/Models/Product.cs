namespace GreenFood.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? SellerId { get; set; }
        public virtual ApplicationUser Seller { get; set; } = null!;
        public string? Header { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public Guid TypeId { get; set; }
        public virtual TypeOfProduct Type { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
