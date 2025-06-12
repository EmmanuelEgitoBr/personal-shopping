using Personal.Shopping.Services.Product.Application.Dtos;

namespace Personal.Shopping.Services.Product.Application.Interfaces;

public interface ICategoryService
{
    Task<ResponseDto> GetAllCategoriesAsync();
    Task<ResponseDto> GetCategoryByIdAsync(int categoryId);
    Task<ResponseDto> CreateCategoryAsync(CategoryDto category);
    Task<ResponseDto> UpdateCategoryAsync(CategoryDto category);
    Task DeleteCategoryAsync(int id);
}
