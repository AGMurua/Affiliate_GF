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
        private readonly IMapper _mapper;

        public AffiliateService(IRepository<AffiliateEntity> affiliateRepository, IMapper mapper)
        {
            _affiliateRepository = affiliateRepository;
            _mapper = mapper;

        }

        public async Task<ICollection<AffiliateWithRelationsDTO>> GetAllWithRelations()
        {
            var data = await _affiliateRepository.GetAllWithRelations();
            List<AffiliateWithRelationsDTO> result = new();
            foreach (AffiliateEntity affiliate in data)
            {
                result.Add(_mapper.Map<AffiliateWithRelationsDTO>(affiliate));
            }
            return result;
        }

        public async Task<ICollection<AffiliateDTO>> GetAll()
        {
            var result = await _affiliateRepository.GetAllAsync();
            return _mapper.Map<ICollection<AffiliateDTO>>(result);
        }

        public async Task<AffiliateDTO> Create(string name)
        {
            AffiliateDTO payload = new()
            {
                Name = name
            };
            var result = await _affiliateRepository.AddAsync(_mapper.Map<AffiliateEntity>(payload));
            return _mapper.Map<AffiliateDTO>(result);
        }
    }
}
