using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Refit;

namespace Personal.Shopping.Services.ShoppingCart.Application.Interfaces;

public interface IProductService
{
    [Get("/api/products")]
    Task<IEnumerable<ProductDto>> GetAllProducs();

    [Get("/api/products/get-by-id/{id}")]
    Task<ProductDto> GetProductByIdAsync(int id);
}
