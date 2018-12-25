using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyCore.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _db;
        DbSet<T> _objectSet;

        public Repository(DbContext db)
        {
            _db = db;
            _objectSet = _db.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }
            return _objectSet;
        }

        public T Find(object id)
        {
            return _objectSet.Find(id);
        }

        public bool Any(object id)
        {
            return Find(id) != null;
        }

        public bool Any(Func<T, bool> predicate)
        {
            return _objectSet.Any(predicate);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _objectSet.FirstOrDefault(predicate);
        }
        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Add(T[] entities)
        {
            foreach (var entity in entities)
            {
                _objectSet.Add(entity);
            }
        }

        public void Update(T entity)
        {
            _objectSet.Attach(entity);
        }

        public void Update(T[] entities)
        {
            foreach (var entity in entities)
            {
                _objectSet.Attach(entity);
            }
        }

        public void Remove(object id)
        {
            var entity = _objectSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            _objectSet.Remove(entity);
        }

        public void Remove(T[] entities)
        {
            foreach (var entity in entities)
            {
                _objectSet.Remove(entity);
            }
        }

        public void Delete(object id)
        {
            var entity = _objectSet.Find(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            _db.Entry(entity).Property("IsDeleted").IsModified = true;
            _db.Entry(entity).Property("IsDeleted").CurrentValue = true;
        }

        public void Restore(object id)
        {
            var entity = _objectSet.Find(id);
            Restore(entity);
        }

        public void Restore(T entity)
        {
            _db.Entry(entity).Property("IsDeleted").IsModified = true;
            _db.Entry(entity).Property("IsDeleted").CurrentValue = false;
        }
    }
}
