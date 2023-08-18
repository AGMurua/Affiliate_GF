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
    public class AffiliateServiceTest
    {
        private Mock<IRepository<AffiliateEntity>> _mockRepositorieAffiliate;
        private IMapper _mapper;
        private IAffiliateService _affiliateService;
        public AffiliateServiceTest()
        {
            _mockRepositorieAffiliate = new Mock<IRepository<AffiliateEntity>>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }).CreateMapper();
            _affiliateService = new AffiliateService(_mockRepositorieAffiliate.Object, _mapper);
        }
        [Fact]
        public async void TestGetAll()
        {
            var affiliates = new List<AffiliateEntity>
            {
                new AffiliateEntity { Id = 1, Name = "Affiliate 1" },
                new AffiliateEntity { Id = 2, Name = "Affiliate 2" }
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetAllAsync()).ReturnsAsync(affiliates);

            var result = await _affiliateService.GetAll();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllWithRelationsTest()
        {
            var affiliates = new List<AffiliateEntity>
            {
                new AffiliateEntity { Id = 1, Name = "Affiliate 1", Customers = new List<CustomerEntity>
                                                                    { new CustomerEntity{Id = 1, Name = "Customer 1", AffiliateId = 1},
                                                                    { new CustomerEntity{Id = 2, Name = "Customer 2", AffiliateId = 1}}}},
                new AffiliateEntity { Id = 2, Name = "Affiliate 2", Customers = new List<CustomerEntity>
                                                                    { new CustomerEntity{Id = 3, Name = "Customer 3", AffiliateId = 2}}},
            };

            _mockRepositorieAffiliate.Setup(repo => repo.GetAllWithRelations()).ReturnsAsync(affiliates);
            var result = await _affiliateService.GetAllWithRelations();
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.FirstOrDefault(x => x.Id == 1).Customers.Count);
            Assert.Equal(1, result.FirstOrDefault(x => x.Id == 2).Customers.Count);
        }
        [Fact]
        public async void Create()
        {
            AffiliateEntity affiliate = new()
            {
                Id = 1,
                Name = "Test Name"
            };
            _mockRepositorieAffiliate.Setup(repo => repo.AddAsync(It.IsAny<AffiliateEntity>())).ReturnsAsync(affiliate);
            var result = await _affiliateService.Create("Test Name");          
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Name", result.Name);
        }
    }
}