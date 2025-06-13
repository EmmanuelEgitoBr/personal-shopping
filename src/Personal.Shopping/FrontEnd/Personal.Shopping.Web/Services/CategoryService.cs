using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.Product;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IBaseService _baseService;

    public CategoryService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateCategoryAsync(CategoryDto category)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = category,
            Url = AppSettings.ProductBaseUrl + $"/api/categories"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> DeleteCategoryAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = AppSettings.ProductBaseUrl + $"/api/categories/delete/{id}"
        });
    }

    public async Task<ResponseDto> GetAllCategorysAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ProductBaseUrl + "/api/categories"
        });
    }

    public async Task<ResponseDto> GetCategoryByIdAsync(int categoryId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ProductBaseUrl + $"/api/categories/get-by-id/{categoryId}"
        });
    }

    public async Task<ResponseDto> UpdateCategoryAsync(CategoryDto category)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.PUT,
            Content = category,
            Url = AppSettings.ProductBaseUrl + $"/api/categories"
        };
        return await _baseService.SendAsync(request!)!;
    }
}
