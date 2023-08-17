using AffiliatesApi.Data.Entities;
using AffiliatesApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AffiliatesApi.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(AffiliateEntity entity)
        {
            _dbContext.Affiliate.Add(entity);
            _dbContext.SaveChanges();
        }

        async Task<IEnumerable<AffiliateEntity>> IRepository.GetAllAsync()
        {
            var result = _dbContext.Affiliate.ToList();
            return result;
        }


    }
}