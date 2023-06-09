﻿
using LMS_API_BusinessLayer.Contracts;
using LMS_API_DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS_API_BusinessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;

            dbSet = _db.Set<T>();

        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            try
            {
                IQueryable<T> query = dbSet;
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

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null,
            int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (pageSize > 0)
                {
                    if (pageSize > 100)
                    {
                        pageSize = 100;
                    }
                    
                    query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                }
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        public async Task RemoveAsync(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                await SaveAsync();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        public async Task SaveAsync()
        {

            await _db.SaveChangesAsync();
        }


    }
}
