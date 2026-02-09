using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infra.Identity.Context;
using NZWalks.Infra.Identity.Domian.Interfaces;
using NZWalks.Infra.Identity.Domian.Models;

namespace NZWalks.Infra.Identity.Repoistories
{
    public class SqlAuthUnitOfWork : IAuthUnitOfWork
    {

        private readonly ZNWalksAuthDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private IAuthRepository? _authRepository;
        private ITokenRepository? _tokenRepository;

        public SqlAuthUnitOfWork(ZNWalksAuthDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IAuthRepository AuthRepository
        {
            get
            {
                if (_authRepository is null)
                {
                    _authRepository = new SqlAuthRepository(_dbContext, _userManager, _roleManager);
                }
                return _authRepository;
            }
        }
        public ITokenRepository TokenRepository
        {
            get
            {
                if (_tokenRepository is null)
                {
                    _tokenRepository = new SqlTokenRepository(_dbContext);
                }
                return _tokenRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
