

using LMS_Utility;
using LMS_WEB.Models;
using LMS_WEB.Models.DTO.Author;
using LMS_WEB.Services.IServices;
using Serilog;

namespace LMS_WEB.Services
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string lmsUrl;

        public AuthorService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            lmsUrl = configuration.GetValue<string>("ServiceUrls:LMSAPI");

        }

        public Task<T> CreateAsync<T>(AuthorCreateDTO dto, string token)
        {
            try
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.POST,
                    Data = dto,
                    Url = lmsUrl + "/api/AuthorAPI",
                    Token = token
                },"AuthorAPI");
            }
            catch (Exception e)
            {
                Log.Information(e.Message);
                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            try
            {
                return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = lmsUrl + "/api/AuthorAPI/" + id,
                    Token = token
                },"AuthorAPI");
            }
            catch (Exception e)
            {

                Log.Information(e.Message);
                Log.Error(e.Message, e.StackTrace); throw new ArgumentException(e.Message);
            }
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/AuthorAPI",
                Token = token
            }, "AuthorAPI");
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = lmsUrl + "/api/AuthorAPI/" + id,
                Token = token
            }, "AuthorAPI");
        }

        public Task<T> UpdateAsync<T>(AuthorDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = lmsUrl + "/api/AuthorAPI/" + dto.AuthorId,
                Token = token
            }, "AuthorAPI");
        }
    }
}
