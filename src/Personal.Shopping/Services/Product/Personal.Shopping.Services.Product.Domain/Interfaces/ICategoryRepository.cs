using Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<IEnumerable<Category>> GetAllCategories();
    Task<Category> GetCategorysById(int categoryNameId);
    Task<Category> UpdateCategory(Category category);
    Task DeleteCategory(int id);
}
