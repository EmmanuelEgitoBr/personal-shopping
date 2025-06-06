using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.Product;

namespace Personal.Shopping.Web.Services;

public class ProductService : IProductService
{
    private readonly IBaseService _baseService;

    public ProductService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateProductAsync(ProductDto product)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = product,
            Url = AppSettings.ProductBaseUrl + $"/api/products"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> DeleteProductAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = AppSettings.ProductBaseUrl + $"/api/products/delete/{id}"
        });
    }

    public async Task<ResponseDto> GetAllProductsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ProductBaseUrl + "/api/products"
        });
    }

    public async Task<ResponseDto> GetProductByNameAsync(string productName)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ProductBaseUrl + $"/api/products/get-by-name/{productName}"
        });
    }

    public async Task<ResponseDto> GetProductByIdAsync(int productId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ProductBaseUrl + $"/api/products/get-by-id/{productId}"
        });
    }

    public async Task<ResponseDto> UpdateProductAsync(ProductDto product)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.PUT,
            Content = product,
            Url = AppSettings.ProductBaseUrl + $"/api/products"
        };
        return await _baseService.SendAsync(request!)!;
    }
}
