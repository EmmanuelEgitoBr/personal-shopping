using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Personal.Shopping.Web.Configurations.Resources;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Auth;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            if(ModelState.IsValid)
            {
                ResponseDto result = await _authService.LoginAsync(model);
                if (result != null && result!.IsSuccess)
                {
                    LoginResponseDto responseDto = JsonConvert
                        .DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result)!)!;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Erro: ", result.Message!);
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

        public IActionResult Logout()
        {
            return View();
        }
    }
}
