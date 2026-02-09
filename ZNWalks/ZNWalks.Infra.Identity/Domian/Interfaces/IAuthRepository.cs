using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infra.Identity.Domian.Models;

namespace NZWalks.Infra.Identity.Domian.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<List<string?>> GetRolesAsync();
        Task<IdentityResult> AddUserToRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
