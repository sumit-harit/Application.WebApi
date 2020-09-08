using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.WebApi.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    { 
        


        Task<T> Get(int id);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> Get(Expression<Func<T, bool>> predicate, params string[] include);


        Task<IReadOnlyList<T>> GetAllAsync();


        Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> predicate, params string[] include);


        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate, params string[] include);
        
                
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity);


        Task UpdateAsync(T entity);
        
        
        Task DeleteAsync(T entity);
        Task DeleteAsync(object id);
        Task DeleteAsync(Expression<Func<T, Boolean>> predicate);
        

    }
}
