using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Interfaces;

namespace AffiliatesApi.Data.Repositories.Interfaces
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<AffiliateEntity>> GetAllWithRelations();
        Task<IEnumerable<CustomerEntity>> GetAllByRelation(int idAffiliate);
        Task<T> AddAsync(T entity);
        Task<int> GetCommisions(int idAffiliated);
        Task<IEntity> GetById(int affiliateId);
    }
}
