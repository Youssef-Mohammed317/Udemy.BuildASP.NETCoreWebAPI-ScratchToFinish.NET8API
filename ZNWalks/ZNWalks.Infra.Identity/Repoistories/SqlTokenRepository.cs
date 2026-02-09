using Microsoft.EntityFrameworkCore;
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
    public class SqlTokenRepository : ITokenRepository
    {
        private readonly ZNWalksAuthDbContext _context;

        public SqlTokenRepository(ZNWalksAuthDbContext context)
        {
            _context = context;
        }

        public async Task AddTokenAsync(TokenStore token)
        {
            await _context.TokenStore.AddAsync(token);

            await _context.SaveChangesAsync();
        }

        public async Task<TokenStore?> GetByJtiAsync(string jti)
        {
            var token = await _context.TokenStore.FirstOrDefaultAsync(t => t.Jti == jti);

            return token;
        }

        public async Task<bool> IsTokenValidAsync(string jti)
        {
            var token = await GetByJtiAsync(jti);

            return token != null && !token.IsRevoked && token.ExpiryDate > DateTime.UtcNow;
        }

        public async Task RevokeTokenAsync(string jti)
        {
            var token = await GetByJtiAsync(jti);
            if (token != null)
            {
                token.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
