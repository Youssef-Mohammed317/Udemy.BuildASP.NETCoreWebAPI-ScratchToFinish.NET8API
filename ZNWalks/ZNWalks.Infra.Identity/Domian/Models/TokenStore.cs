using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Infra.Identity.Domian.Models
{
    public class TokenStore
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Jti { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }
        public string Token { get; set; }
        public bool IsRevoked { get; set; } = false;
        public ApplicationUser User { get; set; } = null!;
    }
}
