using LMS_WEB.Models.DTO.Author;

namespace LMS_WEB.Services.IServices
{
    public interface IAuthorService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(AuthorCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(AuthorDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
