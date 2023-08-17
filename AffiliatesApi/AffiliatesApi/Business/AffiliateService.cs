using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using AffiliatesApi.Model;
using AutoMapper;

namespace AffiliatesApi.Business
{
    public class AffiliateService : IAffiliateService
    {
        private IRepository _repository;
        private readonly IMapper _mapper;

        public AffiliateService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<ICollection<AffiliateDTO>> GetAll()
        {
           return _mapper.Map<IList<AffiliateDTO>>(_repository.GetAllAsync());
        }

        public async Task<ICollection<CustomerDTO>> GetByAffiliateId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async void Create(string name)
        {
            AffiliateDTO payload = new AffiliateDTO(name);
            _repository.Add(_mapper.Map<AffiliateEntity>(payload));
        }
    }
}
