namespace GreenFood.Application.DTO.OutputDto
{
    public class OutputProductDto
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SellerId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
