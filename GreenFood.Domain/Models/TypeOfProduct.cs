namespace GreenFood.Domain.Models
{
    public class TypeOfProduct
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
    }
}
