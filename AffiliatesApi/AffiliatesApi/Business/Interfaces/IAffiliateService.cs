using AffiliatesApi.Data.Entities;
using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface IAffiliateService
    {
        public Task<ICollection<AffiliateDTO>> GetAll();

        public Task<int> Create(string name);

        public Task<ICollection<CustomerDTO>> GetByAffiliateId(Guid id);


    }
}
