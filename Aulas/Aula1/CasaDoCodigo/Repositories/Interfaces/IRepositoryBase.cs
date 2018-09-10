using System;
using System.Collections.Generic;

namespace CasaDoCodigo.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void AddAll(List<TEntity> objs);
        void Dispose();
        TEntity Get(Func<TEntity, bool> expr);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> expr);
        TEntity GetById(int id);
        void Remove(TEntity obj);
        void Update(TEntity obj);
    }
}