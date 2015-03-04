
using System.Collections.Generic;

namespace Signa.Data.Repository
{
    public interface IRepository<TEntity> : IEnumerable<TEntity>
    {
        int Count { get; }
        void Add(TEntity entity);
        TEntity GetByIndex(int index);
        TEntity GetById(string id);
        void Load();
        void SaveChanges();
    }
}