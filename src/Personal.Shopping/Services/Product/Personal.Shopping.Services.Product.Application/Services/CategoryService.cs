using AutoMapper;
using Personal.Shopping.Services.Product.Application.Dtos;
using Personal.Shopping.Services.Product.Application.Interfaces;
using Personal.Shopping.Services.Product.Domain.Entities;
using Personal.Shopping.Services.Product.Domain.Interfaces;

namespace Personal.Shopping.Services.Product.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDto> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategories();

        if (categories is null || categories.Count() == 0)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar as categorias"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<IEnumerable<CategoryDto>>(categories),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategorysById(categoryId);

        if (category is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível encontrar a categoria"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CategoryDto>(category),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> CreateCategoryAsync(CategoryDto category)
    {
        var result = await _categoryRepository.CreateCategory(_mapper.Map<Category>(category));

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
            Result = _mapper.Map<CategoryDto>(category),
            IsSuccess = true
        };
    }

    public async Task<ResponseDto> UpdateCategoryAsync(CategoryDto category)
    {
        var result = await _categoryRepository.UpdateCategory(_mapper.Map<Category>(category));

        if (result is null)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "Não foi possível atualizar a categoria"
            };
        }

        return new ResponseDto
        {
            Result = _mapper.Map<CategoryDto>(category),
            IsSuccess = true
        };
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteCategory(id);
    }
}
