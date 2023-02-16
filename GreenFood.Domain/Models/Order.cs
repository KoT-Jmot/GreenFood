namespace GreenFood.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public DateTime RegDate { get; set; }
    }
}
