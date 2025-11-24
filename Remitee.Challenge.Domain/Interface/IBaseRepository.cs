using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Domain.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<IList<T>> GetAllByAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void ClearTracking();         // Limpia todas las entidades del ChangeTracker
        void Detach(T entity);        // Desasocia una entidad específica
    }
}