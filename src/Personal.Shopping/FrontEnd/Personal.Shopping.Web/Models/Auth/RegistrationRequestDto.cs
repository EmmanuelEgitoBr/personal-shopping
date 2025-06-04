using System.ComponentModel.DataAnnotations;

namespace Personal.Shopping.Web.Models.Auth;

public class RegistrationRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione uma das opções.")]
    public string Role { get; set; } = string.Empty;
}
