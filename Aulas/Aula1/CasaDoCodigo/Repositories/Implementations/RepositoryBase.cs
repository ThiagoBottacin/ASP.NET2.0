using System;
using System.Collections.Generic;
using System.Linq;
using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : BaseModel
    {
        protected readonly ApplicationContext _contexto;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(ApplicationContext contexto)
        {
            _contexto = contexto;
            _dbSet = contexto.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
            _contexto.SaveChanges();
        }

        public void AddAll(List<TEntity> objs)
        {
            foreach (TEntity obj in objs)
            {
                _dbSet.Add(obj);
            }
            _contexto.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(TEntity obj)
        {
            _contexto.Entry(obj).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
            _contexto.SaveChanges();
        }

        public TEntity Get(Func<TEntity, bool> expr)
        {
            return _dbSet.FirstOrDefault(expr);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> expr)
        {
            return _dbSet.ToList().Where(expr);
        }

        public void Dispose()
        {
            _contexto.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
