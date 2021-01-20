using MyGym.Core.Model;
using MyGym.Core.Repositories;

namespace MyGym.Infrastructure.Repositories
{
    /// <summary>
    ///     Customer Repository.
    /// </summary>
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region Constructor
        public CustomerRepository(MyGymDbContext context) : base(context)
        {
        }
        #endregion
    }
}
