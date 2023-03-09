namespace GreenFood.Application.DTO.InputDto
{
    public abstract class BaseQueryDto
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
