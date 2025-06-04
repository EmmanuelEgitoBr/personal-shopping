using Personal.Shopping.Services.Auth.Infra.Models;

namespace Personal.Shopping.Services.Auth.Api.Services.Interfaces;

public interface ITokenService
{
    public string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}
