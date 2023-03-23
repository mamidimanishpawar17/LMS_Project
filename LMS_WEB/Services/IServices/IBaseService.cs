

using LMS_WEB.Models;

namespace LMS_WEB.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest, string serviceName);
    }
}
