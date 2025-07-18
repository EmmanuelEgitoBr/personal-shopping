using Personal.Shopping.Services.Email.Domain.Models.CartModels;

namespace Personal.Shopping.Services.Email.Domain.Interfaces;

public interface IEmailRepository
{
    Task EmailCartAndLog(Cart cart);
}
