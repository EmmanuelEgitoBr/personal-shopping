using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Product.Domain.Entities;
using Personal.Shopping.Services.Product.Domain.Interfaces;
using Personal.Shopping.Services.Product.Infra.Context;

namespace Personal.Shopping.Services.Product.Infra.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Category> CreateCategory(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<Category> GetCategorysById(int categoryNameId)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.CategoryNameId == categoryNameId);
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();

        return category;
    }

    public async Task DeleteCategory(int id)
    {
        var category = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryNameId == id);
        _db.Categories.Remove(category!);
        await _db.SaveChangesAsync();
    }
}
