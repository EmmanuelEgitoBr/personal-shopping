﻿namespace Personal.Shopping.Services.Product.Application.Interfaces;

using Personal.Shopping.Services.Product.Application.Dtos;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

public interface IProductService
{
    Task<ResponseDto> GetAllProductsAsync();
    Task<ResponseDto> GetProductByIdAsync(int productId);
    Task<ResponseDto> GetProductByNameAsync(string productName);
    Task<ResponseDto> CreateProductAsync(ProductDto product);
    Task<ResponseDto> UpdateProductAsync(ProductDto product);
    Task DeleteProductAsync(int id);
}
