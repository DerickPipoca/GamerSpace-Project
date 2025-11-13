namespace GamerSpace.Application.DTOs
{
    public class PagedResultDto<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long TotalRecords { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PagedResultDto(IEnumerable<T> items, long totalRecords, int pageNumber, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);
        }
    }
}