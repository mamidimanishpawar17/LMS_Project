

using LMS_Utility;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO.Member;
using LMS_WEB.Services.IServices;

namespace LMS_WEB.Services
{
    public class MemberService : BaseService, IMemberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string lmsUrl;

        public MemberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            lmsUrl = configuration.GetValue<string>("ServiceUrls:LMSAPI");

        }

        public Task<T> CreateAsync<T>(MemberCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = lmsUrl + "/api/MemberAPI",
                Token = token
            }, "MemberAPI");
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = lmsUrl + "/api/MemberAPI/" + id,
                Token = token
            }, "MemberAPI");
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/MemberAPI",
                Token = token
            }, "MemberAPI");
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/MemberAPI/" + id,
                Token = token
            }, "MemberAPI");
        }

        public Task<T> UpdateAsync<T>(MemberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = lmsUrl + "/api/MemberAPI/" + dto.MemberId,
                Token = token
            }, "MemberAPI");
        }
    }
}
