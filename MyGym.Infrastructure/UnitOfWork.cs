using MyGym.Core;
using MyGym.Core.Repositories;
using MyGym.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Infrastructure
{
    /// <summary>
    ///  Unit Of Work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Variable Declaration.
        private readonly MyGymDbContext _context;
        private CustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public UnitOfWork(MyGymDbContext context)
        {
            this._context = context;
        }
        #endregion

        public ICustomerRepository Customers => _customerRepository = _customerRepository ?? new CustomerRepository(_context);

        /// <summary>
        ///     Save Changes Async.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Dispose.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
