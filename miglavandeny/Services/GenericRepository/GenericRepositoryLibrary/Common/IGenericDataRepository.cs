using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IGenericDataRepository<TContext, TEntity> : IDataRepository<TEntity> where TEntity : class where TContext : class, IDisposable, new()
    {
        TContext DBContext { get; }
    }
}
