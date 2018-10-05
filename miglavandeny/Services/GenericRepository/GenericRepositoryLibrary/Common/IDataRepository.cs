using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericRepository
{
    public interface IDataRepository<T> : IDisposable where T : class
    {

        /// <summary>
        /// Return Entity from the database, use for Entities that have one Primary Key.
        /// </summary>
        /// <param name="Id">Unique Identifier for the Entity</param>
        /// <param name="navigationProperties">Add which properties to include to the Entity</param>
        /// <returns>Entity</returns>
        T GetById(Guid Id, params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// Return Entity from the database, use for Entities that have MORE than one Primary Key.
        /// </summary>
        /// <param name="Ids">Array of unique identifiers</param>
        /// <param name="navigationProperties">Add which properties to include to the Entity returned</param>
        /// <returns>Entity</returns>
        T GetById(object[] Ids, params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// Gets all the Entities in the DBContext.
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns>List of Entities</returns>
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// Gets all the Entities in the DBContext, filtered by the function passed.
        /// </summary>
        /// <param name="where">Function used to filter the entites in the DBContext</param>
        /// <param name="navigationProperties">Add which properties to include to the Entity</param>
        /// <returns>List of Entities</returns>
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// Gets single Entity from the DBContext.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties">Add which properties to include to the Entity</param>
        /// <returns></returns>
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// Count all the Entities in  the database,filtered by a function
        /// </summary>
        /// <param name="where">Filter the entites by a function</param>
        /// <returns></returns>
        int GetCount(Func<T, bool> where);
        /// <summary>
        /// Count all the Entities in the database.
        /// </summary>
        /// <returns>An integer</returns>
        int GetCount();
        /// <summary>
        /// Add Entity(s) to database
        /// </summary>
        /// <param name="items">Array of Entities</param>
        void Add(params T[] items);
        /// <summary>
        /// Update Entity(s) from Database
        /// </summary>
        /// <param name="items">Array of entities</param>
        void Update(params T[] items);
        /// <summary>
        /// Remove Entity(s) from database
        /// </summary>
        /// <param name="items">Array of Entities</param>
        void Remove(params T[] items);
        /// <summary>
        /// Create a new DBContext, and DbContextTransaction inside the repository that will be used for Transactions.
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Saves the DBContext initialised, and commits the changes to the DB.
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Roll back all the changes saved to the context.
        /// </summary>
        void RollBackTransaction();
        /// <summary>
        /// Dispose the DBContext created for transactions, if NULL will not return an error.
        /// </summary>
        void DisposeTransaction();
        /// <summary>
        /// Checks if there's any record in the database filtered by the function
        /// </summary>
        /// <returns>True/False boolean</returns>
        bool Exists(Func<T, bool> where);
    }
}