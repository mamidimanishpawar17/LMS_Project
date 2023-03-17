

using LMS_Utility;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO;
using LMS_WEB.Services.IServices;

namespace LMS_WEB.Services
{
    public class IssueOverDueService : BaseService, IIssueOverDueService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string lmsUrl;

        public IssueOverDueService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            lmsUrl = configuration.GetValue<string>("ServiceUrls:LMSAPI");

        }

        public Task<T> CreateAsync<T>(IssueOverDueCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = lmsUrl + "/api/IssueOverDueAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = lmsUrl + "/api/IssueOverDueAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/IssueOverDueAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/IssueOverDueAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(IssueOverDueDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = lmsUrl + "/api/IssueOverDueAPI/" + dto.OverDueIssueNo,
                Token = token
            });
        }
    }
}
