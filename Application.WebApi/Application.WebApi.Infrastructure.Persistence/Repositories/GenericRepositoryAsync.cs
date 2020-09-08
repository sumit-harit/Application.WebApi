using Application.WebApi.Application.Interfaces;
using Application.WebApi.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.WebApi.Infrastructure.Persistence.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        
        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }       
       
        

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {

            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
                
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return await query.FirstOrDefaultAsync(predicate);
            
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
                
        }

        public async Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> predicate, params string[] include)
        {            
            IQueryable<T> query = _dbContext.Set<T>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
        {
            return await _dbContext
               .Set<T>()
               .Where(predicate)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return await query
                .Where(predicate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity)
        {
            _dbContext.Set<T>().AddRange(entity.ToArray());
            await _dbContext.SaveChangesAsync();
            return entity;
        }


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
