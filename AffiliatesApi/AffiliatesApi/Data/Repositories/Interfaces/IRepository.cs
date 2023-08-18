using AffiliatesApi.Data.Entities;

namespace AffiliatesApi.Data.Repositories.Interfaces
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IList<AffiliateEntity>> GetAllWithRelations();
        Task<IList<CustomerEntity>> GetAllByRelation(int idAffiliate);
        Task<T> AddAsync(T entity);
    }
}
