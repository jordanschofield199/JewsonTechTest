using System;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Newtonsoft.Json;

namespace Jewson.RestService.Client.Extensions
{
    public static class StringExtensions
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(StringExtensions));

        public static T Deserialize<T>(this string content, string url)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unable to convert to type {typeof(T).FullName} coming from url {url} from string {content} || END OF CONTENT");
            }
            return default(T);
        }
    }
}
