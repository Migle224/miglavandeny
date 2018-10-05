using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GenericRepository.Core
{
    public class GenericDataRepository<TContext, TEntity> : IGenericDataRepository<TContext,TEntity> where TEntity : class where TContext : class, IDisposable, new()
    {
        public DbContextOptionsBuilder options;

        DbContext _context;
        IDbContextTransaction _transactionContext;

        public GenericDataRepository()
        {
            options = null;
        }

        public GenericDataRepository(DbContextOptionsBuilder opts)
        {
            options = opts;
        }

        #region Context regeneration

        /// <summary>
        /// Default string connection used, as defined in the context type base constructor.
        /// </summary>
        /// <returns></returns>
        private DbContext NewContextDefault()
        {
            return new TContext() as DbContext;
        }

        /// <summary>
        /// Custom context options.
        /// </summary>
        /// <param name="options">DbContextOptionsBuilder options containing options</param>
        /// <returns></returns>
        private DbContext NewContextWithOptions(DbContextOptionsBuilder options)
        {
            return Activator.CreateInstance(typeof(TContext), new object[] { options.Options }) as DbContext;
        }

        private DbContext NewContext()
        {
            DbContext context;

            context = options != null ? NewContextWithOptions(options) : NewContextDefault();
            return context;
        }

        #endregion

        public bool TransactionActive
        {
            get
            {
                return !(_context == null || _transactionContext == null);
            }
        }

        public TContext DBContext
        {
            get
            {
                return (_context ?? (_context = NewContext())) as TContext;
            }
        }

        public void BeginTransaction()
        {
            if (_context == null)
            {
                _context = NewContext();
            }
            _transactionContext = _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (!TransactionActive)
            {
                throw new Exception("Transaction needs to be initialised");
            }
            try
            {
                _transactionContext.Commit();
            }
            catch
            {
                _transactionContext.Rollback();
            }
            finally
            {
                DisposeTransaction();
            }
        }
        public void RollBackTransaction()
        {
            if (!TransactionActive)
            {
                throw new Exception("Transaction needs to be initialised");
            }
            try
            {
                _transactionContext.Rollback();
            }
            catch { }
            finally
            {
                DisposeTransaction();
            }
        }
        public void DisposeTransaction()
        {
            if (TransactionActive)
            {
                _transactionContext.Dispose();

                _context.Dispose();
            }
            _transactionContext = null;
            _context = null;
        }
        public virtual TEntity GetById(object[] Ids, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity entityObject;
            using (var context = NewContext())
            {
                entityObject = context.Set<TEntity>().Find(Ids);
            }
            return entityObject;
        }
        public virtual TEntity GetById(Guid Id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity entityObject;
            using (var context = NewContext())
            {
                entityObject = context.Set<TEntity>().Find(Id);
            }
            return entityObject;
        }
        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                list = dbQuery.AsNoTracking().ToList<TEntity>();
            }
            return list;
        }
        public virtual IList<TEntity> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();
            }
            return list;
        }
        public virtual TEntity GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                item = dbQuery.AsNoTracking()
                    .FirstOrDefault(where);
            }
            return item;
        }
        public virtual int GetCount(Func<TEntity, bool> where)
        {
            int item = 0;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                item = dbQuery.AsNoTracking().Count(where);
            }
            return item;
        }
        public int GetCount()
        {
            int item = 0;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                item = dbQuery.AsNoTracking().Count();
            }
            return item;
        }
        public virtual void Add(params TEntity[] items)
        {
            if (TransactionActive)
            {
                Add(_context, items);
                _context.SaveChanges();
            }
            else
            {
                using (var context = NewContext())
                {
                    Add(context, items);
                    context.SaveChanges();
                }
            }
        }
        public virtual void Update(params TEntity[] items)
        {
            if (TransactionActive)
            {
                Update(_context, items);
                _context.SaveChanges();
            }
            else
            {
                using (var context = NewContext())
                {
                    Update(context, items);
                    context.SaveChanges();
                }
            }
        }
        public virtual void Remove(params TEntity[] items)
        {
            if (TransactionActive)
            {
                Remove(_context, items);
                _context.SaveChanges();
            }
            else
            {
                using (var context = NewContext())
                {
                    Remove(context, items);
                    context.SaveChanges();
                }
            }
        }
        private void Add<TContextTR>(TContextTR context, params TEntity[] items)
        {

            foreach (TEntity item in items)
            {
                ((DbContext)(object)context).Entry(item).State = EntityState.Added;
            }
        }
        private void Update<TContextTR>(TContextTR context, params TEntity[] items)
        {
            foreach (TEntity item in items)
            {
                ((DbContext)(object)context).Entry(item).State = EntityState.Modified;
            }
        }

        private void Remove<TContextTR>(TContextTR context, params TEntity[] items)
        {
            foreach (TEntity item in items)
            {
                ((DbContext)(object)context).Entry(item).State = EntityState.Deleted;
            }
        }

        public bool Exists(Func<TEntity, bool> where)
        {
            bool exists;
            using (var context = NewContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                exists = dbQuery.AsNoTracking().Any(where);
            }
            return exists;
        }

        #region IDisposable Member

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _transactionContext.Dispose();
                }
                _disposed = true;
            }
        }

        ~GenericDataRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}
