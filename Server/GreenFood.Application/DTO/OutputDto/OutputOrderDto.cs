namespace GreenFood.Application.DTO.OutputDto
{
    public class OutputOrderDto
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public string? CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
