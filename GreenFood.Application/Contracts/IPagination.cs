using GreenFood.Domain.Utils;

namespace GreenFood.Application.Contracts
{
    public interface IPagination
    {
        MetaData? MetaData { get; set; }
    }
}
