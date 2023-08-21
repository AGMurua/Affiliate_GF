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
        private ICustomerService _customerService;
        private CustomerController _customerController;
        public CustomerControllerTest()
        {
            _mockRepositorieAffiliate = new Mock<IRepository<AffiliateEntity>>();
            _mockRepositorieCustomer = new Mock<IRepository<CustomerEntity>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper();

            _customerService = new CustomerService(_mockRepositorieCustomer.Object, _mockRepositorieAffiliate.Object, _mapper);
            _customerController = new CustomerController(_customerService);
        }


        [Fact]
        public async void CreateCustomerTest()
        {

            CustomerCreateDTO payload = new ()
            {
                Name = "Customer Test",
                AffiliateId = 1,
            };
            AffiliateEntity affiliateEntity = new ()
            {
                Id = 1,
                Name = "Affiliate 1"
            };
            CustomerEntity createdCustomer = new ()
            {
                Id = 1,
                Name = "Customer Test",
                AffiliateId = 1
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.AddAsync(It.IsAny<CustomerEntity>())).ReturnsAsync(createdCustomer);
            IActionResult result = await _customerController.CreateCustomer(payload);

            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }
        [Fact]
        public async void CreateCustomerAffiliatedNotFoundTest()
        {

            CustomerCreateDTO payload = new()
            {
                Name = "Customer Test",
                AffiliateId = 1,
            };
            AffiliateEntity affiliateEntity = null;
            CustomerEntity createdCustomer = new()
            {
                Id = 1,
                Name = "Customer Test",
                AffiliateId = 1
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.AddAsync(It.IsAny<CustomerEntity>())).ReturnsAsync(createdCustomer);

            IActionResult result = await _customerController.CreateCustomer(payload);

            UnprocessableEntityObjectResult badRequestResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(422, badRequestResult.StatusCode);
        }
        [Fact]
        public async void CreateCustomerWrongNameTest()
        {
            CustomerCreateDTO payload = new ()
            {
                Name = "Customer Test 1234",
                AffiliateId = 1,
            };

            IActionResult result = await _customerController.CreateCustomer(payload);

            UnprocessableEntityObjectResult badRequestResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(422, badRequestResult.StatusCode);
        }

    }
}