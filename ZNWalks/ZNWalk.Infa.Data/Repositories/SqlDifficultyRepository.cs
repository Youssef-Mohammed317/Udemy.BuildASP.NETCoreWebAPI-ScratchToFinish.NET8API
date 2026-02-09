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
    public class SqlDifficultyRepository : IDifficultyRepository
    {
        private readonly ZNWalksDbContext dbContext;

        public SqlDifficultyRepository(ZNWalksDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task CreateAsync(Difficulty difficulty)
        {
            await dbContext.Difficulty.AddAsync(difficulty);
        }

        public void Delete(Difficulty difficulty)
        {
            dbContext.Difficulty.Remove(difficulty);
        }

        public IQueryable<Difficulty> GetAll()
        {
            return dbContext.Difficulty.AsQueryable();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            return await dbContext.Difficulty
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public void Update(Difficulty difficulty)
        {
            dbContext.Difficulty.Update(difficulty);
        }
    }
}
