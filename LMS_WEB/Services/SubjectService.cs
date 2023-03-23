

using LMS_Utility;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO.Subjects;
using LMS_WEB.Services.IServices;

namespace LMS_WEB.Services
{
    public class SubjectService : BaseService, ISubjectService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string lmsUrl;

        public SubjectService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            lmsUrl = configuration.GetValue<string>("ServiceUrls:LMSAPI");

        }

        public Task<T> CreateAsync<T>(SubjectCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = lmsUrl + "/api/SubjectAPI",
                Token = token
            }, "SubjectAPI");
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = lmsUrl + "/api/SubjectAPI/" + id,
                Token = token
            }, "SubjectAPI");
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/SubjectAPI",
                Token = token
            }, "SubjectAPI");
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/SubjectAPI/" + id,
                Token = token
            }, "SubjectAPI");
        }

        public Task<T> UpdateAsync<T>(SubjectUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = lmsUrl + "/api/SubjectAPI/" + dto.SubjectId,
                Token = token
            }, "SubjectAPI");
        }
    }
}
