using AffiliatesApi.Data.Entities;
using AffiliatesApi.Model;

namespace AffiliatesApi.Business.Interfaces
{
    public interface IAffiliateService
    {
        public Task<ICollection<AffiliateDTO>> GetAll();
        public Task<ICollection<AffiliateWithRelationsDTO>> GetAllWithRelations();

        public Task<AffiliateDTO> Create(string name);



    }
}
