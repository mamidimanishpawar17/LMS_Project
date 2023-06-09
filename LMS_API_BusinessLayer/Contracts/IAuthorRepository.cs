﻿using FluentEmail.Core;
using LMS_API_DataLayer.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {

        Task<Author> UpdateAsync(Author entity);
        Task<List<Author>> GetAll();
        Task<Author> GetById(int id);

    }
}
