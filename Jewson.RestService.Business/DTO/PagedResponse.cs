using System.Collections.Generic;

namespace Jewson.RestService.Business.DTO
{
    /// <summary>
    /// Standard response when wanting to return data using paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        public IList<T> Data { get; set; }
        public int Records => Data?.Count ?? 0;
        public int TotalRecords { get; set; }
    }
}
