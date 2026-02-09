using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRegionRepository RegionRepository { get; }
        IWalkRepository WalkRepository { get; }
        IDifficultyRepository DifficultyRepository { get; }
        IImageRepository ImageRepository { get; }
        Task SaveChangesAsync();
    }
}
