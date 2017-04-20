namespace Jewson.RestService.Business.DTO
{
    /// <summary>
    /// Standard response from API to keep consistancy
    /// </summary>
    /// <typeparam name="T">Generic type therefore reusable</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        /// <summary>
        /// Can populate a useful message to return to the client if needed.
        /// </summary>
        public string Message { get; set; }

        public T Data { get; set; }

        /// <summary>
        /// Optional since we might not always return list data
        /// </summary>
        public int? Records { get; set; }

        /// <summary>
        /// Optional since we might not always return list data
        /// </summary>
        public int? TotalRecords { get; set; }
    }
}
