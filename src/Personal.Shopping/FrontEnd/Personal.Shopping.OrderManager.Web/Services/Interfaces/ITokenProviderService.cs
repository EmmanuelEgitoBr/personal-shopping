namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface ITokenProviderService
{
    void ClearToken();
    string? GetToken();
    void SetToken(string token);
}
