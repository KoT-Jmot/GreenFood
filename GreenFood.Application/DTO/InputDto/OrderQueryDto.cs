namespace GreenFood.Application.DTO.InputDto
{
    public class OrderQueryDto : BaseQueryDto
    {
        public Guid? ProductId { get; set; }
    }
}
