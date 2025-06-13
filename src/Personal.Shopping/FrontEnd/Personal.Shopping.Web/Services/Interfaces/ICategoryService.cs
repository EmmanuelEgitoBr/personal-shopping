using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Product;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface ICategoryService
{
    Task<ResponseDto> CreateCategoryAsync(CategoryDto category);
    Task<ResponseDto> DeleteCategoryAsync(int id);
    Task<ResponseDto> GetAllCategorysAsync();
    Task<ResponseDto> GetCategoryByIdAsync(int categoryId);
    Task<ResponseDto> UpdateCategoryAsync(CategoryDto category);
}
