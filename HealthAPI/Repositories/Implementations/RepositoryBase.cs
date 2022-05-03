using HealthAPI.Data;
using HealthAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HealthAPI.Repositories.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly HealthAPIContext _healthAPIContext;

        public RepositoryBase(HealthAPIContext healthAPIContext)
            => _healthAPIContext = healthAPIContext;
        public void Create(T entity) => _healthAPIContext.Set<T>().Add(entity);

        public void Delete(T entity) => _healthAPIContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _healthAPIContext.Set<T>().AsNoTracking() :
            _healthAPIContext.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            _healthAPIContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
            _healthAPIContext.Set<T>()
            .Where(expression);
        public void Update(T entity) => _healthAPIContext.Set<T>().Update(entity);
    }
}
