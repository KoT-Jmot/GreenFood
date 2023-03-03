namespace GreenFood.Application.DTO.InputDto
{
    public class ProductDto
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
