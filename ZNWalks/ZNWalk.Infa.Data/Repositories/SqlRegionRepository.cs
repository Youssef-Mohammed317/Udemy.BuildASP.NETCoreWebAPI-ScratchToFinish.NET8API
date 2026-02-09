using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infa.Data.Contexts;
using NZWalks.Domain.Interfaces;
using NZWalks.Domain.Models;

namespace NZWalks.Infa.Data.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly ZNWalksDbContext dbContext;

        public SqlRegionRepository(ZNWalksDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task CreateAsync(Region region)
        {
            await dbContext.Region.AddAsync(region);
        }

        public void Delete(Region region)
        {
            dbContext.Region.Remove(region);
        }

        public IQueryable<Region> GetAll()
        {
            return dbContext.Region.AsQueryable();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Region.FindAsync(id);
        }

        public void Update(Region region)
        {
            dbContext.Region.Update(region);
        }

    }
}
