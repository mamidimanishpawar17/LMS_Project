using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Contracts
{
    public interface IIssueRepository : IRepository<Issue>
    {
        
        Task<Issue> GetAsync(Expression<Func<Issue, bool>> filter = null, bool tracked = true, string includeProperties = null);
        //Task SendReminder();
        Task<List<Issue>> GetOverdueIssues();
        //Task SendReminder(IMessageSender messageSender, string recipient, string subject, string body);
        Task<Issue> UpdateAsync(Issue entity);
        Task<List<Issue>> GetAll();
        Task<Issue> GetById(int id);

    }
  
}
