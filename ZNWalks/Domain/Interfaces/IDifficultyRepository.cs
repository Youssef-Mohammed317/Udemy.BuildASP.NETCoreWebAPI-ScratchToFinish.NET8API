using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Domain.Models;

namespace NZWalks.Domain.Interfaces
{
    public interface IDifficultyRepository
    {
        public Task CreateAsync(Difficulty difficulty);

        public void Delete(Difficulty difficulty);
        public IQueryable<Difficulty> GetAll();

        public Task<Difficulty?> GetByIdAsync(Guid id);

        public void Update(Difficulty difficulty);
    }
}
