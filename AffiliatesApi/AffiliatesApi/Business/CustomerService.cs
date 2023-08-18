using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Business
{
    public class CustomerService : ICustomerService
    {
        private IRepository<CustomerEntity> _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<CustomerEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<int> Create(CustomerCreateDTO payload)
        {
            var result = await _repository.AddAsync(_mapper.Map<CustomerEntity>(payload));
            return result.Id;
        }
    }
}
