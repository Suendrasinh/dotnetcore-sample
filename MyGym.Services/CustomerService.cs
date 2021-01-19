using MyGym.Core;
using MyGym.Core.Model;
using MyGym.Core.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGym.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer;
        }

        public async Task Update(Guid id, Customer customer)
        {
            Customer dbCustomer = await GetById(id);
            if (dbCustomer != null)
            {
                dbCustomer.FirstName = customer.FirstName;
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Customer dbCustomer = await GetById(id);
            _unitOfWork.Customers.Remove(dbCustomer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
