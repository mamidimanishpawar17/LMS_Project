
using LMS_WEB.Models.DTO;

namespace LMS_WEB.Services.IServices
{
    public interface IMemberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(MemberCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(MemberUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
