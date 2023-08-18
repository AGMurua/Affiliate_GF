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
    public class AffiliateControllerTest
    {
        private Mock<IRepository<AffiliateEntity>> _mockRepositorieAffiliate;
        private Mock<IRepository<CustomerEntity>> _mockRepositorieCustomer;
        private IMapper _mapper;
        private IAffiliateService _affiliateService;
        private ICustomerService _customerService;
        private AffiliateController _affiliateController;
        public AffiliateControllerTest()
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
        public async void GetAllAffiliates()
        {
            var affiliates = new List<AffiliateEntity>
            {
                new AffiliateEntity { Id = 1, Name = "Affiliate 1" },
                new AffiliateEntity { Id = 2, Name = "Affiliate 2" }
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetAllAsync()).ReturnsAsync(affiliates);

            var result = await _affiliateController.GetAll() as ActionResult;
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAffiliates = Assert.IsType<List<AffiliateDTO>>(okResult.Value);
            Assert.Equal(2, returnedAffiliates.Count);
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

        [Fact]
        public async void GetAffiliateCustomersTest()
        {
            var affiliatesCustomers = new List<CustomerEntity>
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };
            var affiliateEntity = new AffiliateEntity { Id = 1, Name = "Affiliate 1" };

            var payload = new AffiliateCreateDTO
            {
                Name = "Affiliate Test"
            };

            var createdAffiliate = new AffiliateEntity { Id = 1, Name = "Affiliate 1" };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetAllByRelation(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers);
            
            var result = await _affiliateController.GetAffiliateCustomers(createdAffiliate.Id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAffiliates = Assert.IsType<List<CustomerDTO>>(okResult.Value);
            Assert.Equal(2, returnedAffiliates.Count);
        }

        [Fact]
        public async void GetCommissionTest()
        {
            var affiliatesCustomers = new List<CustomerEntity>
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };
            var affiliateEntity = new AffiliateEntity { Id = 1, Name = "Affiliate 1" };

            var payload = new AffiliateCreateDTO
            {
                Name = "Affiliate Test"
            };

            var createdAffiliate = new AffiliateEntity { Id = 1, Name = "Affiliate 1" };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetCommisions(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers.Count());

            var result = await _affiliateController.GetCommission(createdAffiliate.Id);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAffiliates = Assert.IsType<int>(okResult.Value);
            Assert.Equal(2, returnedAffiliates);
        }
    }
}