using Microsoft.AspNetCore.Identity;
using Personal.Shopping.Services.Auth.Api.Models.Dtos;
using Personal.Shopping.Services.Auth.Api.Services.Interfaces;
using Personal.Shopping.Services.Auth.Infra.Context;
using Personal.Shopping.Services.Auth.Infra.Models;

namespace Personal.Shopping.Services.Auth.Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(AppDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.Email!,
                    Id = userToReturn.Id,
                    Name = userToReturn!.Name!,
                    PhoneNumber = userToReturn.PhoneNumber!
                };

                return userDto;
            }
        }
        catch (Exception)
        {
            throw;
        }

        return new UserDto();
    }
}
