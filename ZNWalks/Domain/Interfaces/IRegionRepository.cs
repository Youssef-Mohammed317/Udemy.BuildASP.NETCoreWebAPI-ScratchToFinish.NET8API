using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Domain.Models;

namespace NZWalks.Domain.Interfaces
{
    public interface IRegionRepository
    {
        IQueryable<Region> GetAll();
        Task<Region?> GetByIdAsync(Guid id);
        Task CreateAsync(Region region);
        void Update(Region region);
        void Delete(Region region);
    }
}
