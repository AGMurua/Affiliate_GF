using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Business
{
    public class AffiliateService : IAffiliateService
    {
        private IRepository<AffiliateEntity> _affiliateRepository;
        private IRepository<CustomerEntity> _customerRepository;
        private readonly IMapper _mapper;

        public AffiliateService(IRepository<AffiliateEntity> affiliateRepository, IMapper mapper)
        {
            _affiliateRepository = affiliateRepository;
            _mapper = mapper;

        }

        public async Task<ICollection<AffiliateDTO>> GetAll()
        {
            List<AffiliateEntity> data = (List<AffiliateEntity>)await _affiliateRepository.GetAllByRelation();
            List<AffiliateDTO> result = new List<AffiliateDTO>();
            foreach (AffiliateEntity affiliate in data)
            {
                result.Add(_mapper.Map<AffiliateDTO>(affiliate));
            }
            return result;
        }

        public async Task<ICollection<CustomerDTO>> GetByAffiliateId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(string name)
        {
            AffiliateDTO payload = new AffiliateDTO(name);
            var result = await _affiliateRepository.AddAsync(_mapper.Map<AffiliateEntity>(payload));
            return result.Id;
        }
    }
}
