using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.OrderManager.Web.Models.Auth;

public class RegistrationRequestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione uma das opções.")]
    public string Role { get; set; } = string.Empty;
}
