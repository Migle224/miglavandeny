using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericRepository
{
    /// <summary>
    /// Controller-level interface for handling repository operations, and ensuring access
    /// to context (if pre-defined) and wrappers around repository operations. 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IGenericRepository<TContext, TEntity> where TContext : class where TEntity : class
    {
        void CreateRepository();

        TEntity Get(Guid id, string caller = null);
        IList<TEntity> GetDetailsList(Func<TEntity, bool> where, string caller = null, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity Add(TEntity entity, string caller = null);
        TEntity Update(TEntity entity, string caller = null);
        void Delete(Guid entity, string caller = null);

        // Helper methods
        Guid[] GetKey<T>(T type);
    }
}
