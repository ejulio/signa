using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Data
{
    public interface IRepository<TEntity>
    {
        int Count { get; }
        void Add(TEntity entity);
        TEntity GetByIndex(int index);
        TEntity GetById(string id);
        void Load();
        void SaveChanges();
    }
}