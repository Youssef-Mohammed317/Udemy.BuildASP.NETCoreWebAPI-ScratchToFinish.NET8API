using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infra.Identity.Context;
using NZWalks.Infra.Identity.Domian.Interfaces;
using NZWalks.Infra.Identity.Domian.Models;

namespace NZWalks.Infra.Identity.Repoistories
{
    public class SqlAuthRepository : IAuthRepository
    {
        private readonly ZNWalksAuthDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public SqlAuthRepository(ZNWalksAuthDbContext _context,
            UserManager<ApplicationUser> _userManager,
            RoleManager<ApplicationRole> _roleManager)
        {
            context = _context;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<List<string?>> GetRolesAsync()
        {
            var roles = await context.Roles.Select(r => r.NormalizedName).ToListAsync();

            return roles;
        }

        public async Task<IdentityResult> AddUserToRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CheckPasswordAsync(user, password);

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            return result;
        }
        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            var result = await userManager.DeleteAsync(user);

            return result;
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }
    }
}
