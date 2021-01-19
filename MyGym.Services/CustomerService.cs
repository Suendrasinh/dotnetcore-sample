using MyGym.Core;
using MyGym.Core.Model;
using MyGym.Core.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyGym.Core.Entity;
using MyGym.Core.Mapper;

namespace MyGym.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAll()
        {
            IEnumerable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();
            return MapperConfiguration.Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetById(Guid id)
        {
            Customer customer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return MapperConfiguration.Mapper.Map<CustomerResponse>(customer);
        }

        public async Task<Guid> Add(SaveCustomerRequest request)
        {
            Customer customer = MapperConfiguration.Mapper.Map<Customer>(request);
            customer.CreatedBy = Guid.NewGuid();
            customer.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer.Id;
        }

        public async Task Update(Guid id, UpdateCustomerRequest request)
        {
            Customer dbCustomer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            Customer customerToUpdate = MapperConfiguration.Mapper.Map(request, dbCustomer);
            customerToUpdate.ModifiedBy = Guid.NewGuid();
            customerToUpdate.ModifiedDate = DateTime.UtcNow;
            _unitOfWork.Customers.Update(customerToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Customer dbCustomer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            _unitOfWork.Customers.Remove(dbCustomer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
