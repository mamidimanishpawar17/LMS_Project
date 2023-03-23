using FluentEmail.Core;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
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
   
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Author> UpdateAsync(Author entity)
        {
            try
            {
                
                _db.Authors.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }
        public async Task<List<Author>> GetAll()
        {
            try
            {
                return await _db.Authors.FromSqlRaw<Author>(ApiStrings.getAuthors).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Error getting entity ");
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Author> GetById(int id)
        {
           

            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var mail = _db.Authors.FirstOrDefault(c => c.AuthorId == id);
                if (mail == null)
                {

                    throw new ApiException("Data Not Found");

                }
                return mail;

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
