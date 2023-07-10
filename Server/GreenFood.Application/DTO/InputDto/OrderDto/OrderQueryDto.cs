namespace GreenFood.Application.DTO.InputDto.OrderDto
{
    public class OrderQueryDto : BaseQueryDto
    {
        public Guid? ProductId { get; set; }
    }
}
