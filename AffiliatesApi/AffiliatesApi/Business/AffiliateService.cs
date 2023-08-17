using AffiliatesApi.Business.Interfaces;
using AffiliatesApi.Model;

namespace AffiliatesApi.Business
{
    public class AffiliateService : IAffiliateService
    {
        public async Task<ICollection<AffiliateDTO>> GetAll()
        {
           return new List<AffiliateDTO>();
        }

        public async void Create()
        {
           
        }

    }
}
