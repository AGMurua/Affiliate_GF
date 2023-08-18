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

        public AffiliateController(IAffiliateService affiliateService)
        {
            _affiliateService = affiliateService ?? throw new ArgumentNullException(nameof(affiliateService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _affiliateService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody, Required]string name)
        {
            var result = await _affiliateService.Create(name);
            return  Ok(Request.Path + "/" + result);
        }
    }
}
