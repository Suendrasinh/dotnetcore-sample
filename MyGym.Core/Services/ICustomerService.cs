using MyGym.Core.Entity;
using MyGym.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Core.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponse>> GetAll();
        Task<CustomerResponse> GetById(Guid id);
        Task<Guid> Add(SaveCustomerRequest request);
        Task Update(Guid id, UpdateCustomerRequest request);
        Task Delete(Guid id);
    }
}
