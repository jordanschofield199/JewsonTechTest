using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Jewson.RestService.Client.Extensions;
using Jewson.RestService.Client.Interfaces;

namespace Jewson.RestService.Client
{
    /// <summary>
    /// Base class for creating a basic HTTP Client wrapper
    /// </summary>
    public class WebApiClient : IWebApiClient
    {
        private readonly IAppSettings _appSettings;
        private readonly ILogger _logger = LogManager.GetLogger<WebApiClient>();

        public WebApiClient(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public T Get<T>(string url)
        {
            var request = CreateWebRequest(url, HttpMethod.Get);
            var response = (request.GetResponse()) as HttpWebResponse;

            return ReadResponse<T>(response);
        }

        public Task<T> GetAsync<T>(string url)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest payload)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest payload)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(string url)
        {
            throw new NotImplementedException();
        }

        private HttpWebRequest CreateWebRequest(string url, HttpMethod method)
        {
            var serviceUrl = _appSettings.GetSetting("ServiceEndpoint");

            var path = $"{serviceUrl}/{url}";
            var uri = new Uri(path, UriKind.Absolute);

            var request = WebRequest.Create(uri) as HttpWebRequest;
            if (request != null)
            {
                request.Method = method.ToString();
                request.ContentType = "application/json";
                request.KeepAlive = false;
                request.CookieContainer = new CookieContainer();
            }

            return request;
        }

        private T ReadResponse<T>(HttpWebResponse response)
        {
            try
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var data = reader.ReadToEnd();
                    if (typeof(T) == typeof(string)) return (T)(object)data;
                    return data.Deserialize<T>(response.ResponseUri.ToString());
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return default(T);
            }
        }
    }
}
