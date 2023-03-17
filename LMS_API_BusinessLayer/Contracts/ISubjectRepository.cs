using LMS_API_DataLayer.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
    public  interface ISubjectRepository : IRepository<Subject>
    {

        Task<Subject> UpdateAsync(Subject entity);

    }
    
}
