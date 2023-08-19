using AffiliatesApi.Business;
using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Common;
using AffiliatesApi.Controllers;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AffiliatesApiTest
{
    public class CustomerServiceTest
    {
        private Mock<IRepository<AffiliateEntity>> _mockRepositorieAffiliate;
        private Mock<IRepository<CustomerEntity>> _mockRepositorieCustomer;
        private IMapper _mapper;
        private ICustomerService _customerService;
        public CustomerServiceTest()
        {
            _mockRepositorieAffiliate = new Mock<IRepository<AffiliateEntity>>();
            _mockRepositorieCustomer = new Mock<IRepository<CustomerEntity>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper();
            _customerService = new CustomerService(_mockRepositorieCustomer.Object, _mockRepositorieAffiliate.Object, _mapper);
        }

        [Fact]
        public async void CreateSuccesfull()
        {
            CustomerCreateDTO payload = new()
            {
                AffiliateId = 1,
                Name = "Test Name Customer"
            };
            CustomerEntity customerToAdd = new()
            {
                Id = 3,
                AffiliateId = 1,
                Name = "Test Name Added Customer"
            };
            AffiliateEntity affiliateLink = new()
            {
                Id = 1,
                Name = "Test name"
            };
            _mockRepositorieAffiliate.Setup(repo => repo.GetById(payload.AffiliateId)).ReturnsAsync(affiliateLink);
            _mockRepositorieCustomer.Setup(repo => repo.AddAsync(It.IsAny<CustomerEntity>())).ReturnsAsync(customerToAdd);
            CustomerDTO result = await _customerService.Create(payload);


            Assert.Equal(3, result.Id);
            Assert.Equal("Test Name Added Customer", result.Name);

        }
        [Fact]
        public async void CreateUnsuccesfull()
        {
            CustomerCreateDTO payload = new()
            {
                AffiliateId = 3,
                Name = "Test Name Customer"
            };
            CustomerEntity customerToAdd = new()
            {
                Id = 3,
                AffiliateId = 1,
                Name = "Test Name Added Customer"
            };
            AffiliateEntity affiliateLink = null;
            _mockRepositorieAffiliate.Setup(repo => repo.GetById(payload.AffiliateId)).ReturnsAsync(affiliateLink);
            _mockRepositorieCustomer.Setup(repo => repo.AddAsync(It.IsAny<CustomerEntity>())).ReturnsAsync(customerToAdd);
            CustomerDTO result = await _customerService.Create(payload);
            Assert.Equal(null, result);
        }

        [Fact]
        public async void GetCommisionReportTest()
        {
            AffiliateEntity affiliateLink = new()
            {
                Id = 1,
                Name = "Affiliate 1"
            };
            List<CustomerEntity> customers = new()
            {
                new CustomerEntity
                {
                  Id = 1,
                  AffiliateId = 1,
                  Name = "Customer 1"
                },
                new CustomerEntity
                {
                  Id = 2,
                  AffiliateId = 1,
                  Name = "Customer 2"
                },

            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetById(affiliateLink.Id)).ReturnsAsync(affiliateLink);
            _mockRepositorieCustomer.Setup(repo => repo.GetCommisions(affiliateLink.Id)).ReturnsAsync(customers.Count);

            int? result = await _customerService.GetCommisionReport(affiliateLink.Id);
            Assert.Equal(2, result);
        }
    }
}