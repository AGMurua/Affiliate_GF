using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AffiliatesApi.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _entities;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _entities.ToListAsync();

            return result;
        }

        public async Task<IList<CustomerEntity>> GetAllByRelation(int idAffiliate)
        {
            var response = await _entities.OfType<CustomerEntity>()
                             .Where(x => x.AffiliateId == idAffiliate).ToListAsync();
            return response;
        }

        public async Task<IList<AffiliateEntity>> GetAllWithRelations()
        {
            var response = await _entities.OfType<AffiliateEntity>()
               .Include(a => a.Customers)
               .ToListAsync();
            return response;
        }
    }
}