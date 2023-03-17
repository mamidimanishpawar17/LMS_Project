
using LMS_WEB.Models.DTO;

namespace LMS_WEB.Services.IServices
{
    public interface IIssueService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(IssueCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(IssueUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
