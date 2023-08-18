using AffiliatesApi.Business;
using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Common;
using AffiliatesApi.Controllers;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControllerApiTest
{
    public class CustomerControllerTest
    {
        private Mock<IRepository<AffiliateEntity>> _mockRepositorieAffiliate;
        private Mock<IRepository<CustomerEntity>> _mockRepositorieCustomer;
        private IMapper _mapper;
        private IAffiliateService _affiliateService;
        private ICustomerService _customerService;
        private AffiliateController _affiliateController;
        public CustomerControllerTest()
        {
            _mockRepositorieAffiliate = new Mock<IRepository<AffiliateEntity>>();
            _mockRepositorieCustomer = new Mock<IRepository<CustomerEntity>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper();

            _customerService = new CustomerService(_mockRepositorieCustomer.Object, _mockRepositorieAffiliate.Object, _mapper);
            _affiliateService = new AffiliateService(_mockRepositorieAffiliate.Object, _mapper);
            _affiliateController = new AffiliateController(_affiliateService, _customerService);
        }


        [Fact]
        public async void CreateAffiliatesTest()
        {

            var payload = new AffiliateCreateDTO
            {
                Name = "Affiliate Test"
            };

            var createdAffiliate = new AffiliateEntity { Id = 1, Name = "Affiliate 1" };

            _mockRepositorieAffiliate.Setup(repo => repo.AddAsync(It.IsAny<AffiliateEntity>())).ReturnsAsync(createdAffiliate);
            var result = await _affiliateController.Create(payload);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

    }
}