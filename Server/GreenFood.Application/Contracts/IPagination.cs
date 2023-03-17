using GreenFood.Application.RequestFeatures;

namespace GreenFood.Application.Contracts
{
    public interface IPagination
    {
        MetaData? MetaData { get; set; }
    }
}
