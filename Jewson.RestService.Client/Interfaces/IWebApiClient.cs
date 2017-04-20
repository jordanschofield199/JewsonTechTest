using System.Threading.Tasks;

namespace Jewson.RestService.Client.Interfaces
{
    public interface IWebApiClient
    {
        T Get<T>(string url);
        Task<T> GetAsync<T>(string url);
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest payload);
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest payload);
        Task<T> DeleteAsync<T>(string url);
    }
}
