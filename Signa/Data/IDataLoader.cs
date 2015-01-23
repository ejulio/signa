using Signa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Signa.Data
{
    public interface IDataLoader
    {
        IList<Sign> Data { get; }
        void Load();
    }
}