using FluentEmail.Core;
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using LMS_API_DataLayer.Models.Books;
using LMS_API_DataLayer.Models.Issues;
using LMS_API_DataLayer.Models.Members;
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
  
   public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly ApplicationDbContext _db;
        public MemberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Member> UpdateAsync(Member entity)
        {
            try
            {

                _db.Members.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        public async Task<List<Member>> GetAll()
        {
            try
            {
                return await _db.Members.FromSqlRaw<Member>(ApiStrings.getMembers).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Error getting entity ");
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Member> GetById(int id)
        {


            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var data = _db.Members.FirstOrDefault(c => c.MemberId == id);
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
