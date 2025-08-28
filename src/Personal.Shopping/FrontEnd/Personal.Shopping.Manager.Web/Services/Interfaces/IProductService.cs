using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Product;
using Refit;

namespace Personal.Shopping.Manager.Web.Services.Interfaces;

public interface IProductService
{
    [Get("/api/products")]
    Task<ResponseDto> GetAllProductsAsync();

    [Get("/api/products/get-by-name/{productName}")]
    Task<ResponseDto> GetProductByNameAsync(string productName);

    [Get("/api/products/get-by-id/{productId}")]
    Task<ResponseDto> GetProductByIdAsync(int productId);

    [Post("/api/products")]
    Task<ResponseDto> CreateProductAsync([Body] ProductDto product);

    [Put("/api/products")]
    Task<ResponseDto> UpdateProductAsync([Body] ProductDto product);

    [Delete("/api/products/delete/{id}")]
    Task<ResponseDto> DeleteProductAsync(int id);
}
