using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Controllers.CustomController;
using AffiliatesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AffiliatesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffiliateController : CustomControllerBase
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

        [HttpGet("GetAllWithRelations")]
        public async Task<IActionResult> GetAllWithRelations()
        {
            return Ok(await _affiliateService.GetAllWithRelations());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AffiliateCreateDTO payload)
        {
            if(!CheckRegexName(payload.Name))
            {
                return BadRequest(nameErrorMsg);
            }
            var result = await _affiliateService.Create(TrimName(payload.Name));
            return CreatedAtAction("Create", result); 
        }

        [HttpGet("{id}/Customers")]
        public async Task<IActionResult> GetAffiliateCustomers(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            ICollection<CustomerDTO> result = await _customerService.GetCustomersByAffiliateId(id);
            if (result is null)
            {
                return NotFound(idNotFoundMsg);
            }
            return Ok(result);
        }

        [HttpGet("{id}/Commisions")]
        public async Task<IActionResult> GetCommission([Required] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await _customerService.GetCommisionReport(id);
            if (result is null)
            {
                return NotFound(idNotFoundMsg);
            }
            return Ok(result);
        }

    }
}
