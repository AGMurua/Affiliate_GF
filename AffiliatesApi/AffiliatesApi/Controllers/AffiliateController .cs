﻿using AffiliatesApi.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            return Ok(_affiliateService.GetAll());
        }
    }
}
