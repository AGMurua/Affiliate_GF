using AffiliatesApi.Data.Entities;

namespace AffiliatesApi.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<AffiliateEntity>> GetAllAsync();
        void Add(AffiliateEntity entity);
    }
}
