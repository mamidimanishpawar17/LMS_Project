using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Repositories
{
    public class IssueOverDueRepository : Repository<IssueOverDue>, IIssueOverDueRepository
    {
        private readonly ApplicationDbContext _db;
        public IssueOverDueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<IssueOverDue> UpdateAsync(IssueOverDue entity)
        {
            try
            {

                _db.OverDues.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        public async Task<List<IssueOverDue>> GetOverdueIssues()
        {
            try
            {
                var overdueIssues = await _db.OverDues
                    .Where(issue => issue.returnDate == null && issue.DueDate < DateTime.Now)
                    .ToListAsync();

                foreach (var issue in overdueIssues)
                {
                    // Update the overdue issue entity
                   
                    issue.OverDueIssueNo = CalculateOverdueFine(issue.DueDate);
                }

                await _db.SaveChangesAsync();

                return overdueIssues;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        private int CalculateOverdueFine(DateTime dueDate)
        {
            var daysOverdue = (DateTime.Now - dueDate).Days;
            var dailyFineAmount = 10;
            var overdueFine = daysOverdue * dailyFineAmount;

            return (int)overdueFine;
        }
    }
}
