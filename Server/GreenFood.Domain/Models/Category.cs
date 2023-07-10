namespace GreenFood.Domain.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
    }
}
