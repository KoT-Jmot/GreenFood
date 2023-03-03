namespace GreenFood.Application.DTO.ServicesDto
{
    public class OrderDto
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; }
        public string? CustomerId { get; set; }
    }
}
