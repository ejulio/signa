using Signa.Model;
using System.Collections.Generic;

namespace Signa.Data
{
    public interface IDataLoader
    {
        IList<Sign> Data { get; }
        void Load();
    }
}