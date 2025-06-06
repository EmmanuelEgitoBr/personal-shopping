namespace Personal.Shopping.Services.Product.Domain.Interfaces;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

public interface IProductRepository
{
    Task<Entity.Product> CreateProduct(Entity.Product product);
    Task<IEnumerable<Entity.Product>> GetAllProducts();
    Task<Entity.Product> GetProductsById(int productId);
    Task<Entity.Product> GetProductByName(string productCode);
    Task<Entity.Product> UpdateProduct(Entity.Product product);
    Task DeleteProduct(int id);
}
