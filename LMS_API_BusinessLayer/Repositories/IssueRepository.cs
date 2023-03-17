using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_BusinessLayer.Repositories
{

    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly EmailMessageSender _messageSender;
        public IssueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Issue> GetAsync(Expression<Func<Issue, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            try
            {
                IQueryable<Issue> query = dbSet;
                if (!tracked)
                {
                    query = query.AsNoTracking();
                }
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        public async Task<Issue> UpdateAsync(Issue entity)
        {
            try
            {

                _db.Issues.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }


        public async Task SendOverdueEmailsAsync()
        {
            var overdueBooks = await _db.Issues
                .Include(b => b.Member)
                .Where(b => b.returnDate == null && b.DueDate < DateTime.Now)
                .ToListAsync();

            foreach (var book in overdueBooks)
            {
                var toEmailAddress = book.Member.Email;
                var subject = "Overdue Book Notification";
                var body = $"Dear {book.Member.FirstName},<br><br>Your book '{book.Book}' is overdue. Please return it as soon as possible.<br><br>Thank you.";

                await _messageSender.SendAsync(toEmailAddress, subject, body);
            }
        }

    }
}
