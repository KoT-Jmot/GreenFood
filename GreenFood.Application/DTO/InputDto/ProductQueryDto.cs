namespace GreenFood.Application.DTO.InputDto
{
    public class ProductQueryDto : BaseQueryDto
    {
        public string Header { get; set; } = null!;
        public int? Count { get; set; }
        public decimal? Price { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
