namespace Personal.Shopping.Manager.Web.Models.Auth;

public class AssignRoleRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
