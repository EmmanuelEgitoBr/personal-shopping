using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.OrderManager.Web.Models.Auth;

public class LoginRequestDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
