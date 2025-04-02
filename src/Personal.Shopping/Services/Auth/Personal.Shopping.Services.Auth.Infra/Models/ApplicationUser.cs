using Microsoft.AspNetCore.Identity;

namespace Personal.Shopping.Services.Auth.Infra.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}
