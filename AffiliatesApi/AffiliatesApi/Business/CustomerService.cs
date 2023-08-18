using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Business
{
    public class CustomerService : ICustomerService
    {
        private IRepository<CustomerEntity> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<CustomerEntity> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        public async Task<int> Create(CustomerCreateDTO payload)
        {
            var result = await _customerRepository.AddAsync(_mapper.Map<CustomerEntity>(payload));
            return result.Id;
        }

        public async Task<ICollection<CustomerDTO>> GetCustomersByAffiliateId(int idAffiliated)
        {
            var result = await _customerRepository.GetAllByRelation(idAffiliated);
            return _mapper.Map<IList<CustomerDTO>>(result);
        }

        public async Task<int> GetCommisionReport(int idAffiliated)
        {
            return await _customerRepository.GetCommisions(idAffiliated);
            
        }
    }
}
