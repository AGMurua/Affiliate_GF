using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AffiliatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliateController : ControllerBase
    {
        private readonly IAffiliateService _affiliateService;
        private readonly ICustomerService _customerService;
        public AffiliateController(IAffiliateService affiliateService, ICustomerService customerService)
        {
            _affiliateService = affiliateService ?? throw new ArgumentNullException(nameof(affiliateService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _affiliateService.GetAll());
        }

        [HttpGet("api/Affiliate/Relations")]
        public async Task<IActionResult> GetAllWithRelations()
        {
            return Ok(await _affiliateService.GetAllWithRelations());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody, Required] string name)
        {
            var result = await _affiliateService.Create(name);
            return Ok(result);
        }

        [HttpGet("{id}/Customers")]
        public async Task<IActionResult> GetAffiliateCustomers(int id)
        {
            var result = await _customerService.GetCustomersByAffiliateId(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/Commisions")]
        public async Task<IActionResult> GetCommission(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await _customerService.GetCommisionReport(id);
            return Ok(result);
        }

    }
}
