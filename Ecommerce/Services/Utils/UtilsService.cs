using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Services.Utils
{
    public class UtilsService : IUtilsService
    {
        private readonly EcommerceDbContext _context;
        public UtilsService(EcommerceDbContext context)
        {
            _context = context;
        }

        public Account ParseJwtToAccountObject(string authSt)
        {
            var jwt = authSt.ToString().Split(" ").ElementAt(1);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var email = token.Claims.ElementAt(0).Value;
            var currentAccount = _context.Accounts.FirstOrDefault(u => u.Email == email);

            return currentAccount;
        }
    }
}
