using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
    public  interface IMemberRepository : IRepository<Member>
    {

        Task<Member> UpdateAsync(Member entity);

    }
  
}
