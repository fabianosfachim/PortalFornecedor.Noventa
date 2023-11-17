using Microsoft.EntityFrameworkCore;
using PortalFornecedor.Noventa.Data.Interfaces;
using PortalFornecedor.Noventa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PortalFornecedor.Noventa.Data.Repositories
{
    public class EntityBaseRepository<T> : IEntityRepository<T> where T : EntityBase
    {
        private readonly ApplicationContext _context;

        public EntityBaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _context.Set<T>();


            if (includes != null)
            {
                foreach (Expression<Func<T, object>> inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            return await query.Where(predicate).ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
            catch { }
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
            _context.SaveChanges();
        }

        public async void AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateRange(IEnumerable<T> entities) { _context.UpdateRange(entities); _context.SaveChanges(); }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public void Remove(T entity) { _context.Remove(entity); _context.SaveChanges(); }

        public void Remove(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }
        public async Task<int> RemoveAsync(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Remove(entity);
            return await _context.SaveChangesAsync();

        }

        public void RemoveRange(IEnumerable<T> entities) { _context.RemoveRange(entities); _context.SaveChanges(); }


    }
}
