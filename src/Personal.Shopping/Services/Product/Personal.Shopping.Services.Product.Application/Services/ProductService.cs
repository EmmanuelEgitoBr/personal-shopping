using AutoMapper;
using Personal.Shopping.Services.Product.Application.Dtos;
using Personal.Shopping.Services.Product.Application.Interfaces;
using Personal.Shopping.Services.Product.Domain.Interfaces;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
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
            await _productRepository.DeleteProduct(id);

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
}
