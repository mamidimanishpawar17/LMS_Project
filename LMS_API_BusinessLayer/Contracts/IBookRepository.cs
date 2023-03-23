using LMS_API_DataLayer.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
  
    public interface IBookRepository : IRepository<Book>
    {

        Task<Book> UpdateAsync(Book entity);
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
    }
}
