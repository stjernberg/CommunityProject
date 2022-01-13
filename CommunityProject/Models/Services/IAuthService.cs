using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommunityProject.Models.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(AppUser user, IList<string> userRoles, IEnumerable<Claim> claims);
    }
}
