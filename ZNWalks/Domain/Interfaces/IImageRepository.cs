using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Domain.Models;

namespace NZWalks.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task CreateAsync(Image image);
        Task<Image?> GetByIdAsync(Guid id);
        void Delete(Image image);
    }
}
