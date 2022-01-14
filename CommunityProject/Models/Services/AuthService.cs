using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


        public string GenerateJwtToken(AppUser user, IList<string> userRoles, IEnumerable<Claim> claims)
        {
            List<Claim> claimsList = new List<Claim>();
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claimsList.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claimsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsList.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in userRoles)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role));
            }

            claimsList.AddRange(claims);

            int expiraionDays = _configuration.GetValue<int>("AuthConfiguration:TokenExpirationDays");
            byte[] signingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AuthConfiguration:SigningKey"));
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AuthConfiguration:Issuer"),
                audience: _configuration.GetValue<string>("AuthConfiguration:Audience"),
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(expiraionDays),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
    
}
