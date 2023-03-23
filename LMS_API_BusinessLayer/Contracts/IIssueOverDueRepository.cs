using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
    public interface IIssueOverDueRepository : IRepository<IssueOverDue>
    {

        Task<IssueOverDue> UpdateAsync(IssueOverDue entity);
        Task<List<IssueOverDue>> GetOverdueIssues();
        
    }
}
