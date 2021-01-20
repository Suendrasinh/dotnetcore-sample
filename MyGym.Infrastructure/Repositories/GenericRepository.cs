using MyGym.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyGym.Infrastructure.Repositories
{
    /// <summary>
    ///     Generic Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Variable Declaration
        protected readonly DbContext Context;
        #endregion

        #region Constructor
        public GenericRepository(DbContext context)
        {
            this.Context = context;
        }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Add Async.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        ///     Update.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///     Add Range Async.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        /// <summary>
        ///     Find.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        /// <summary>
        ///     Get All Async.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        /// <summary>
        ///     Remove.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        /// <summary>
        ///     Remove Range.
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        ///     Single Or Default Async.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().SingleOrDefaultAsync(predicate);
        }
        #endregion
    }
}
