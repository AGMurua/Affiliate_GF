using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Interfaces;
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

        public async Task<IEnumerable<CustomerEntity>> GetAllByRelation(int idAffiliate)
        {
            var response = await _entities.OfType<CustomerEntity>()
                                          .Where(x => x.AffiliateId == idAffiliate)
                                          .ToListAsync();
            return response;
        }

        public async Task<IEnumerable<AffiliateEntity>> GetAllWithRelations()
        {
            var response = await _entities.OfType<AffiliateEntity>()
                                          .Include(a => a.Customers)
                                          .ToListAsync();
            return response;
        }

        public async Task<IEntity> GetById(int affiliateId)
        {
            var result = await _entities.OfType<IEntity>()
                                        .FirstOrDefaultAsync(x => x.Id == affiliateId);

            return result;
        }

        public async Task<int> GetCommisions(int idAffiliated)
        {
            var response = await _entities.OfType<CustomerEntity>()
                                          .CountAsync(x => x.AffiliateId == idAffiliated);
            return response;
        }
    }
}