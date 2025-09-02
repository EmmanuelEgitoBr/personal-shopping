using Personal.Shopping.OrderManager.Web.Models;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface IProductApiClient
{
    [Get("/api/products")]
    Task<ResponseDto> GetAllProductsAsync();

    [Get("/api/products/get-by-name/{productName}")]
    Task<ResponseDto> GetProductByNameAsync(string productName);

    [Get("/api/products/get-by-id/{productId}")]
    Task<ResponseDto> GetProductByIdAsync(int productId);
}
