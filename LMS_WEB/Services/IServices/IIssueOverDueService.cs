using LMS_WEB.Models.DTO.Issue;

namespace LMS_WEB.Services.IServices
{
    public interface IIssueOverDueService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(IssueOverDueCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(IssueOverDueDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
