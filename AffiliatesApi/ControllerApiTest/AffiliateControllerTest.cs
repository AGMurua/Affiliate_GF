using AffiliatesApi.Business;
using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Common;
using AffiliatesApi.Controllers;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;
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
            List<AffiliateEntity> affiliates = new()
            {
                new AffiliateEntity { Id = 1, Name = "Affiliate 1" },
                new AffiliateEntity { Id = 2, Name = "Affiliate 2" }
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetAllAsync()).ReturnsAsync(affiliates);

            IActionResult result = await _affiliateController.GetAll() as ActionResult;
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            List<AffiliateDTO> returnedAffiliates = Assert.IsType<List<AffiliateDTO>>(okResult.Value);
            Assert.Equal(2, returnedAffiliates.Count);
        }

        [Fact]
        public async void CreateAffiliatesTest()
        {

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.AddAsync(It.IsAny<AffiliateEntity>())).ReturnsAsync(createdAffiliate);
            IActionResult result = await _affiliateController.Create(payload);

            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }
        [Fact]
        public async void CreateAffiliatesWrongNameTest()
        {

            var payload = new AffiliateCreateDTO
            {
                Name = "Affiliate Test 1234"
            };

            IActionResult? result = await _affiliateController.Create(payload);

            UnprocessableEntityObjectResult badRequestResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(422, badRequestResult.StatusCode);
        }

        [Fact]
        public async void GetAffiliateCustomersTest()
        {
            List<CustomerEntity> affiliatesCustomers = new()
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };

            AffiliateEntity affiliateEntity = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetAllByRelation(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers);

            IActionResult? result = await _affiliateController.GetAffiliateCustomers(createdAffiliate.Id);
            OkObjectResult? okResult = Assert.IsType<OkObjectResult>(result);
            List<CustomerDTO>? returnedAffiliates = Assert.IsType<List<CustomerDTO>>(okResult.Value);
            Assert.Equal(2, returnedAffiliates.Count);
        }

        [Fact]
        public async void GetAffiliateCustomersNoContentTest()
        {
            List<CustomerEntity> affiliatesCustomers = new();

            AffiliateEntity affiliateEntity = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetAllByRelation(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers);

            IActionResult? result = await _affiliateController.GetAffiliateCustomers(createdAffiliate.Id);
            NoContentResult? noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async void GetAffiliateCustomersWrongIdTest()
        {
            List<CustomerEntity> affiliatesCustomers = new()
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };
            AffiliateEntity affiliateEntity = null;

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetAllByRelation(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers);

            IActionResult? result = await _affiliateController.GetAffiliateCustomers(createdAffiliate.Id);

            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }


        [Fact]
        public async void GetCommissionTest()
        {
            List<CustomerEntity> affiliatesCustomers = new()
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };
            AffiliateEntity affiliateEntity = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetCommisions(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers.Count());

            IActionResult result = await _affiliateController.GetCommission(createdAffiliate.Id);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            int returnedAffiliates = Assert.IsType<int>(okResult.Value);
            Assert.Equal(2, returnedAffiliates);
        }

        [Fact]
        public async void GetCommissionWrongIdTest()
        {
            List<CustomerEntity> affiliatesCustomers = new()
            {
              { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1} },
              { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1} }
            };
            AffiliateEntity affiliateEntity = null;

            AffiliateCreateDTO payload = new()
            {
                Name = "Affiliate Test"
            };

            AffiliateEntity createdAffiliate = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(affiliateEntity);
            _mockRepositorieCustomer.Setup(repo => repo.GetCommisions(It.IsAny<int>())).ReturnsAsync(affiliatesCustomers.Count());

            IActionResult result = await _affiliateController.GetCommission(createdAffiliate.Id);

            NotFoundObjectResult notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);

        }
    }
}