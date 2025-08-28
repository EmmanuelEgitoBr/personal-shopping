using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Product;
using Refit;

namespace Personal.Shopping.Manager.Web.Services.Interfaces;

public interface ICategoryService
{
    [Get("/api/categories")]
    Task<ResponseDto> GetAllCategoriesAsync();

    [Get("get-by-id/{id}")]
    Task<ResponseDto> GetCategoryByIdAsync(int id);

    [Post("/api/categories")]
    Task<ResponseDto> CreateCategoryAsync([Body] CategoryDto category);

    [Put("/api/categories")]
    Task<ResponseDto> UpdateCategoryAsync([Body] CategoryDto category);

    [Delete("delete/{id}")]
    Task<ResponseDto> DeleteCategoryAsync(int id);
}
