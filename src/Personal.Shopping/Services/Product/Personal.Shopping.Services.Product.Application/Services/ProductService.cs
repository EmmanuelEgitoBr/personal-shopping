using AutoMapper;
using Microsoft.AspNetCore.Http;
using Personal.Shopping.Services.Product.Application.Dtos;
using Personal.Shopping.Services.Product.Application.Interfaces;
using Personal.Shopping.Services.Product.Domain.Interfaces;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IAwsS3BucketService _awsS3BucketService;
    private IMapper _mapper;

    public ProductService(IProductRepository productRepository, 
        IAwsS3BucketService awsS3BucketService, 
        IMapper mapper)
    {
        _productRepository = productRepository;
        _awsS3BucketService = awsS3BucketService;
        _mapper = mapper;
    }

    public async Task<ResponseDto> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProducts();

        if (products is null || products.Count() == 0)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar os produtos"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<IEnumerable<ProductDto>>(products),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> GetProductByIdAsync(int productId)
    {
        var product = await _productRepository.GetProductsById(productId);

        if (product is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar o produto"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<ProductDto>(product),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> GetProductByNameAsync(string productName)
    {
        var product = await _productRepository.GetProductByName(productName);

        if (product is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar o produto"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<ProductDto>(product),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> CreateProductAsync(ProductDto product)
    {
        var result = await _productRepository.CreateProduct(_mapper.Map<Entity.Product>(product));

        if (result is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível criar o produto"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<ProductDto>(product),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> UpdateProductAsync(ProductDto product)
    {
        var result = await _productRepository.UpdateProduct(_mapper.Map<Entity.Product>(product));

        if (result is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível atualizar o produto"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<ProductDto>(product),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> DeleteProductAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetProductsById(id);

            await _productRepository.DeleteProduct(id);

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var oldFile = Path.GetFileName(new Uri(product.ImageUrl).LocalPath);
                await _awsS3BucketService.DeleteFileAsync(oldFile);
            }

            return new ResponseDto()
            {
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto() {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseDto> UploadProductImageAsync(int productId, IFormFile imageFile)
    {
        try
        {
            var product = await _productRepository.GetProductsById(productId);

            if (product == null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Produto não encontrado"
                };
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var oldFile = Path.GetFileName(new Uri(product.ImageUrl).LocalPath);
                await _awsS3BucketService.DeleteFileAsync(oldFile);
            }

            var fileName = $"products/{productId}_{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

            var url = await _awsS3BucketService.UploadFileAsync(imageFile, fileName);

            product.ImageUrl = url;
            await _productRepository.UpdateProduct(product);

            return new ResponseDto
            {
                IsSuccess = true,
                Result = url,
                Message = "Arquivo salvo com êxito!"
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}
