using FluentEmail.Core;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Strings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio.Exceptions;

namespace LMS_API_BusinessLayer.Repositories
{

    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IIssueRepository _issueRepository;
        private readonly IConfiguration _configuration;
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

        public async Task<List<Issue>> GetOverdueIssues()
        {
            try
            {
                return await _db.Issues
                       .Include(issue => issue.Member)
                       .Where(issue => issue.returnDate == null && issue.DueDate < DateTime.Now)
                       .ToListAsync();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }
        public async Task<List<Issue>> GetAll()
        {
            try
            {
                return await _db.Issues.FromSqlRaw<Issue>(ApiStrings.getIssues).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Error getting entity ");
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Issue> GetById(int id)
        {


            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var data = _db.Issues.FirstOrDefault(c => c.IssueId == id);
                if (data == null)
                {

                    throw new ApiException("Data Not Found");

                }
                return data;

            }
            catch (ApiException e)
            {
                transaction.Commit();

                throw new ApiException(e.Message);
            }
            finally
            {
                _db.Dispose();
            }
        }


    }
}
