using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AffiliatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDTO payload)
        {
           var result = await _customerService.Create(payload);
           return Ok();
        }
    }
}
