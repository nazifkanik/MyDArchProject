
using Business.Handlers.CustomerCustomerDemoes.Commands;
using Business.Handlers.CustomerCustomerDemoes.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// CustomerCustomerDemoes If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCustomerDemoesController : BaseApiController
    {
        ///<summary>
        ///List CustomerCustomerDemoes
        ///</summary>
        ///<remarks>CustomerCustomerDemoes</remarks>
        ///<return>List CustomerCustomerDemoes</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerCustomerDemo>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetCustomerCustomerDemoesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>CustomerCustomerDemoes</remarks>
        ///<return>CustomerCustomerDemoes List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerCustomerDemo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int customerId)
        {
            var result = await Mediator.Send(new GetCustomerCustomerDemoQuery { CustomerId = customerId.ToString() });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add CustomerCustomerDemo.
        /// </summary>
        /// <param name="createCustomerCustomerDemo"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCustomerDemoCommand createCustomerCustomerDemo)
        {
            var result = await Mediator.Send(createCustomerCustomerDemo);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update CustomerCustomerDemo.
        /// </summary>
        /// <param name="updateCustomerCustomerDemo"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCustomerDemoCommand updateCustomerCustomerDemo)
        {
            var result = await Mediator.Send(updateCustomerCustomerDemo);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete CustomerCustomerDemo.
        /// </summary>
        /// <param name="deleteCustomerCustomerDemo"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCustomerDemoCommand deleteCustomerCustomerDemo)
        {
            var result = await Mediator.Send(deleteCustomerCustomerDemo);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
