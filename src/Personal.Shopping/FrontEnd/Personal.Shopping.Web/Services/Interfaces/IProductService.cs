using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.Product;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDto> CreateProductAsync(ProductDto product);
    Task<ResponseDto> DeleteProductAsync(int id);
    Task<ResponseDto> GetAllProductsAsync();
    Task<ResponseDto> GetProductByNameAsync(string productName);
    Task<ResponseDto> GetProductByIdAsync(int productId);
    Task<ResponseDto> UpdateProductAsync(ProductDto product);
}
