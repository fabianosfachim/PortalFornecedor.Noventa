using System.Linq.Expressions;

namespace PortalFornecedor.Noventa.Data.Interfaces
{
    public interface IEntityRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        void AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> RemoveAsync(int id);
        Task<T> UpdateAsync(T entity);


    }
}
