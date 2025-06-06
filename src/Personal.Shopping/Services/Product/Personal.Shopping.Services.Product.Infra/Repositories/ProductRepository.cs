using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Product.Domain.Interfaces;
using Personal.Shopping.Services.Product.Infra.Context;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Entity.Product> CreateProduct(Entity.Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return product;
    }

    public async Task<IEnumerable<Entity.Product>> GetAllProducts()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Entity.Product> GetProductsById(int productId)
    {
        return await _db.Products.FirstOrDefaultAsync(c => c.ProductId == productId);
    }

    public async Task<Entity.Product> GetProductByName(string productCode)
    {
        return await _db.Products.FirstOrDefaultAsync(c => c.Name == productCode!);
    }

    public async Task<Entity.Product> UpdateProduct(Entity.Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();

        return product;
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(c => c.ProductId == id);
        _db.Products.Remove(product!);
        await _db.SaveChangesAsync();
    }
}
