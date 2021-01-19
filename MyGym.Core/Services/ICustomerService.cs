using MyGym.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Core.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(Guid id);
        Task<Customer> Add(Customer customer);
        Task Update(Guid id, Customer customer);
        Task Delete(Guid id);
    }
}
