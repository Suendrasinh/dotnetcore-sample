using MyGym.Core;
using MyGym.Core.Entity;
using MyGym.Core.Mapper;
using MyGym.Core.Model;
using MyGym.Core.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGym.Services
{
    /// <summary>
    /// Customer Service.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        #region Variable Declaration
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Get All.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerResponse>> GetAll()
        {
            IEnumerable<Customer> customers = await _unitOfWork.Customers.GetAllAsync();
            return MapperConfiguration.Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(customers);
        }

        /// <summary>
        ///     Get By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> GetById(Guid id)
        {
            Customer customer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return MapperConfiguration.Mapper.Map<CustomerResponse>(customer);
        }

        /// <summary>
        ///     Add.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Guid> Add(SaveCustomerRequest request)
        {
            Customer customer = MapperConfiguration.Mapper.Map<Customer>(request);
            customer.CreatedBy = Guid.NewGuid();
            customer.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return customer.Id;
        }

        /// <summary>
        ///     Update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Update(Guid id, UpdateCustomerRequest request)
        {
            Customer dbCustomer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            Customer customerToUpdate = MapperConfiguration.Mapper.Map(request, dbCustomer);
            customerToUpdate.ModifiedBy = Guid.NewGuid();
            customerToUpdate.ModifiedDate = DateTime.UtcNow;
            _unitOfWork.Customers.Update(customerToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        ///     Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            Customer dbCustomer = await _unitOfWork.Customers.SingleOrDefaultAsync(x => x.Id.Equals(id));
            _unitOfWork.Customers.Remove(dbCustomer);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
