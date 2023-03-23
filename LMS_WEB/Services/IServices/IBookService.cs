using LMS_WEB.Models.DTO.Book;

namespace LMS_WEB.Services.IServices
{
    public interface IBookService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(BookCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(BookUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
