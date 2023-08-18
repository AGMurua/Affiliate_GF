using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Controllers.CustomController;
using AffiliatesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AffiliatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDTO payload)
        {
            if (!CheckRegexName(payload.Name))
            {
                return BadRequest(nameErrorMsg);
            }
            payload.Name = TrimName(payload.Name);
            var result = await _customerService.Create(payload);
            if(result is null)
            {
                return BadRequest(idNotFoundMsg);
            }
            return Created(Request.Path + "/" + result.Id, result);
        }
    }
}
