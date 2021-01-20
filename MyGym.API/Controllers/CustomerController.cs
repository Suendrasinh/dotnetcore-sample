using Microsoft.AspNetCore.Mvc;
using MyGym.API.Validators;
using MyGym.Core.Entity;
using MyGym.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGym.API.Controllers
{
    /// <summary>
    ///     Customer Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Variable Declaration
        /// <summary>
        ///     Customer Service.
        /// </summary>
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor
        /// <summary>
        ///     Customer Controller.
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Get All.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }

        /// <summary>
        ///     Get By Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetById(Guid id)
        {
            return Ok(await _customerService.GetById(id));
        }

        /// <summary>
        ///     Save.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<CustomerResponse>> Save([FromBody] SaveCustomerRequest request)
        {
            var validator = new SaveCustomerValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Created("", await _customerService.Add(request));
        }

        /// <summary>
        ///     Update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            var validator = new UpdateCustomerValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (id == Guid.Empty || !validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok(_customerService.Update(id,request));
        }

        /// <summary>
        ///     Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _customerService.Delete(id);

            return NoContent();
        }
        #endregion
    }
}
