using AffiliatesApi.Data.Entities;

namespace AffiliatesApi.Data.Repositories.Interfaces
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IList<AffiliateEntity>> GetAllByRelation();
        Task<T> AddAsync(T entity);
    }
}
