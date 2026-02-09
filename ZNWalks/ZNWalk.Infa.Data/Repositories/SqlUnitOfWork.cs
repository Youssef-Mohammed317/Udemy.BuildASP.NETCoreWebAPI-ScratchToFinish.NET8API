using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infa.Data.Contexts;
using NZWalks.Domain.Interfaces;

namespace NZWalks.Infa.Data.Repositories
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly ZNWalksDbContext dbContext;
        private IRegionRepository? regionRepository;
        private IDifficultyRepository? difficultyRepository;
        private IWalkRepository? walkRepository;
        private IImageRepository? imageRepository;

        public SqlUnitOfWork(ZNWalksDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IRegionRepository RegionRepository
        {
            get
            {
                if (regionRepository is null)
                {
                    regionRepository = new SqlRegionRepository(dbContext);
                }
                return regionRepository;
            }
        }
        public IWalkRepository WalkRepository
        {
            get
            {
                if (walkRepository is null)
                {
                    walkRepository = new SqlWalkRepository(dbContext);
                }
                return walkRepository;
            }
        }
        public IDifficultyRepository DifficultyRepository
        {
            get
            {
                if (difficultyRepository is null)
                {
                    difficultyRepository = new SqlDifficultyRepository(dbContext);
                }
                return difficultyRepository;
            }
        }
        public IImageRepository ImageRepository
        {
            get
            {
                if (imageRepository is null)
                {
                    imageRepository = new SqlImageRepository(dbContext);
                }
                return imageRepository;
            }
        }
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
