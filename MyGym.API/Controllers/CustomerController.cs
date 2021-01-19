using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyGym.API.Validators;
using MyGym.Core.Entity;
using MyGym.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAllMusics()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetById(Guid id)
        {
            return Ok(await _customerService.GetById(id));
        }

        [HttpPost("")]
        public async Task<ActionResult<CustomerResponse>> Create([FromBody] SaveCustomerRequest request)
        {
            var validator = new SaveCustomerValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Created("", await _customerService.Add(request));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            var validator = new UpdateCustomerValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (id == Guid.Empty || !validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            return Ok(_customerService.Update(id,request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusic(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _customerService.Delete(id);

            return NoContent();
        }
    }
}
