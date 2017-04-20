using System;
using System.Net.Http;
using System.Threading.Tasks;
using Jewson.Logging;
using Jewson.Logging.Interfaces;

namespace Jewson.RestService.Client.Extensions
{
    public static class HttpContentExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(HttpContentExtensions));

        public static async Task<string> ReadAsString(this HttpContent content, string url)
        {
            try
            {
                return await content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unable to read string from url: {url}");
            }
            return string.Empty;
        }
    }
}
