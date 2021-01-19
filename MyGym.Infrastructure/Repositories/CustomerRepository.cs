using MyGym.Core.Model;
using MyGym.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGym.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyGymDbContext context) : base(context)
        {
        }
    }
}
