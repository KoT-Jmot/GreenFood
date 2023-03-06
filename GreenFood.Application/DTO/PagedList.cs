using GreenFood.Application.Contracts;
using GreenFood.Domain.Utils;

namespace GreenFood.Application.DTO
{
    public class PagedList<T> : List<T>, IPagination
    {
        public MetaData? MetaData { get; set; }

        public PagedList(
            IEnumerable<T> items,
            int totalCount,
            int currentPage)
        {
            var pageSize = items.Count();
            var totalPages = totalCount / pageSize;

            MetaData = new MetaData(currentPage, totalPages, pageSize, totalCount);

            AddRange(items);
        }
        public static PagedList<T> ToPagedList(
            IEnumerable<T> source,
            int currentPage,
            int pageSize,
            int totalCount)
        { 
            return new PagedList<T>(source, totalCount, currentPage);
        }
    }

}
