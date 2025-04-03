using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<ResponseDto> AssignRoleToUser(string email, string roleName)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email!.ToLower() == email!.ToLower());

        if (user is not null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return new ResponseDto
            {
                IsSuccess = true,
                Result = roleName
            };
        }

        return new ResponseDto
        {
            IsSuccess = false,
            Message = "Não foi possível atribuir role ao usuário"
        };
    }

    public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName!.ToLower() == loginRequestDto.UserName.ToLower());

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user!, loginRequestDto.Password);

        if (user is null || !isPasswordValid)
        {
            return new ResponseDto { 
                IsSuccess = false,
                Message = "Dados incorretos"
            };
        }

        var token = _tokenService.GenerateToken(user!);

        if (token.IsNullOrEmpty())
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Token inválido"
            };
        }

        UserDto userDto = new()
        {
            Email = user.Email!,
            Id = user.Id,
            Name = user!.Name!,
            PhoneNumber = user.PhoneNumber!
        };

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDto,
            Token = token
        };

        return new ResponseDto
        {
            IsSuccess = true,
            Result = loginResponseDto
        };
    }

    public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
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

                if (userToReturn is not null)
                {
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email!,
                        Id = userToReturn.Id,
                        Name = userToReturn!.Name!,
                        PhoneNumber = userToReturn.PhoneNumber!
                    };

                    return new ResponseDto()
                    {
                        IsSuccess = true,
                        Result = userDto
                    };
                }
            }
        }
        catch (Exception)
        {
            new ResponseDto
            {
                IsSuccess = false,
                Message = "Erro ao registrar usuário"
            };
        }

        return new ResponseDto
        {
            IsSuccess = false,
            Message = "Não foi possível registrar usuário"
        };
    }
}
