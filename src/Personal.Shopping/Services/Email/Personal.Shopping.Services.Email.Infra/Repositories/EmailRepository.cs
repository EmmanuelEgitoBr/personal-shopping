using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Email.Domain.Entities;
using Personal.Shopping.Services.Email.Domain.Interfaces;
using Personal.Shopping.Services.Email.Domain.Models.CartModels;
using Personal.Shopping.Services.Email.Infra.Context;
using System.Text;

namespace Personal.Shopping.Services.Email.Infra.Repositories;

public class EmailRepository : IEmailRepository
{
    private DbContextOptions<AppDbContext> _dbOptions;

    public EmailRepository(DbContextOptions<AppDbContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }

    public async Task EmailCartAndLog(Cart cart)
    {
        StringBuilder message = new StringBuilder();

        message.AppendLine("<br/>Carrinho de compras");
        message.AppendLine("<br/>Total: " + cart.CartHeader.CartTotal);
        message.Append("<br/>");
        message.Append("<ul>");
        foreach(var item in cart.CartDetails)
        {
            message.Append("<li>");
            message.Append(item.Product.Name + " x " + item.Count);
            message.Append("</li>");
        }
        message.Append("</ul>");

        await LogAndEmail(message.ToString(), cart.CartHeader.Email!);
    }

    private async Task<bool> LogAndEmail(string message, string emailAddress)
    {
        try
        {
            EmailLogger emailLog = new()
            {
                Email = emailAddress,
                EmailSent = DateTime.Now,
                Message = message
            };

            await using var _db = new AppDbContext(_dbOptions);
            _db.EmailLoggers.Add(emailLog);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
