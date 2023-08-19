using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Interfaces;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Business
{
    public class CustomerService : ICustomerService
    {
        private IRepository<CustomerEntity> _customerRepository;
        private IRepository<AffiliateEntity> _affiliateRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<CustomerEntity> customerRepository, IRepository<AffiliateEntity> affiliateRepository, IMapper mapper)
        {
            _affiliateRepository = affiliateRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

        public async Task<CustomerDTO> Create(CustomerCreateDTO payload)
        {
            IEntity affiliateExist = await _affiliateRepository.GetById(payload.AffiliateId);
            if(affiliateExist is null)
            {
                return null;
            }
            CustomerEntity result = await _customerRepository.AddAsync(_mapper.Map<CustomerEntity>(payload));
            return _mapper.Map<CustomerDTO>(result);
        }

        public async Task<ICollection<CustomerDTO>> GetCustomersByAffiliateId(int idAffiliated)
        {
            IEntity affiliateExist = await _affiliateRepository.GetById(idAffiliated);
            if (affiliateExist is null)
            {
                return null;
            }
            IEnumerable<CustomerEntity> result = await _customerRepository.GetAllByRelation(idAffiliated);
            return _mapper.Map<IList<CustomerDTO>>(result);
        }

        public async Task<int?> GetCommisionReport(int idAffiliated)
        {
            IEntity affiliateExist = await _affiliateRepository.GetById(idAffiliated);
            if (affiliateExist is null)
            {
                return null;
            }
            return await _customerRepository.GetCommisions(idAffiliated);
            
        }
    }
}
