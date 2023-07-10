namespace GreenFood.Application.DTO.InputDto
{
    public abstract class BaseQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
