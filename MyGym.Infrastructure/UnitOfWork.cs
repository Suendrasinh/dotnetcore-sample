using MyGym.Core;
using MyGym.Core.Repositories;
using MyGym.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyGymDbContext _context;
        private CustomerRepository _customerRepository;

        public UnitOfWork(MyGymDbContext context)
        {
            this._context = context;
        }

        public ICustomerRepository Customers => _customerRepository = _customerRepository ?? new CustomerRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
