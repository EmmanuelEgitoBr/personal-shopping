using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Personal.Shopping.OrderManager.Web.Configurations.Resources;
using Personal.Shopping.OrderManager.Web.Models;
using Personal.Shopping.OrderManager.Web.Models.Auth;
using Personal.Shopping.OrderManager.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Personal.Shopping.OrderManager.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthApiClient _authService;
        private readonly ITokenProviderService _tokenProvider;

        public AuthController(IAuthApiClient authService, ITokenProviderService tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto result = await _authService.LoginAsync(model);
                if (result != null && result!.IsSuccess)
                {
                    LoginResponseDto responseDto = JsonConvert
                        .DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result)!)!;

                    await SignInUserAsync(responseDto);
                    _tokenProvider.SetToken(responseDto.Token!);

                    return RedirectToAction("OrderLogIndex", "OrderLog");
                }
                else
                {
                    ModelState.AddModelError("Erro: ", result!.Message!);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{Text = RoleConstants.RoleAdmin, Value = RoleConstants.RoleAdmin},
                new SelectListItem{Text = RoleConstants.RoleCustomer, Value = RoleConstants.RoleCustomer}
            };
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto result = await _authService.RegisterAsync(model);

                if (result != null && result.IsSuccess)
                {
                    if (String.IsNullOrEmpty(model.Role))
                    {
                        model.Role = RoleConstants.RoleCustomer;
                    }
                    var assignRole = await _authService.AssignRole(new AssignRoleRequestDto
                    {
                        Email = model.Email,
                        RoleName = model.Role
                    });
                    if (assignRole != null && assignRole.IsSuccess)
                    {
                        TempData["success"] = ResponseConstants.AccountCreated;
                        return RedirectToAction(nameof(Login));
                    }
                }
            }

            var roles = new List<SelectListItem>()
            {
                new SelectListItem{Text = RoleConstants.RoleAdmin, Value = RoleConstants.RoleAdmin},
                new SelectListItem{Text = RoleConstants.RoleCustomer, Value = RoleConstants.RoleCustomer}
            };
            ViewBag.Roles = roles;

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUserAsync(LoginResponseDto loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponse.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)!.Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role")!.Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
