using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Strings;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Exceptions;

namespace LMS_API_BusinessLayer.Repositories
{
  
   public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly ApplicationDbContext _db;
        public SubjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Subject> UpdateAsync(Subject entity)
        {
            try
            {

                _db.Subjects.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }
        public async Task<List<Subject>> GetAll()
        {
            try
            {
                return await _db.Subjects.FromSqlRaw<Subject>(ApiStrings.getSubjects).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Error getting entity ");
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Subject> GetById(int id)
        {


            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var data = _db.Subjects.FirstOrDefault(c => c.SubjectId == id);
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
