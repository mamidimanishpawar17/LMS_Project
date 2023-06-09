﻿using LMS_WEB.Models.DTO.Subjects;

namespace LMS_WEB.Services.IServices
{
    public interface ISubjectService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(SubjectCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(SubjectUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
