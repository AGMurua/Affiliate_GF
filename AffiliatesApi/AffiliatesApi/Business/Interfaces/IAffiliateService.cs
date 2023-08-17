using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface IAffiliateService
    {
        public Task<ICollection<AffiliateDTO>> GetAll();

        public void Create(string name);

        public Task<ICollection<CustomerDTO>> GetByAffiliateId(Guid id);


    }
}
