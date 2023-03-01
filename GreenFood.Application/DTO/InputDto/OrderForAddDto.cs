namespace GreenFood.Application.DTO.InputDto
{
    public class OrderForAddDto
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; }
        public string? CustomerId { get; set; }
    }
}
