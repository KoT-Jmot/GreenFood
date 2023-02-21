namespace GreenFood.Domain.Models
{
    public class TypeOfProduct : BaseEntity
    {
        public string? Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
    }
}
