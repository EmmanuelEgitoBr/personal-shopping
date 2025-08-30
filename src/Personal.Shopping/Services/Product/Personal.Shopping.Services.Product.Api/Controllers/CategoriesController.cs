using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Product.Application.Dtos;
using Personal.Shopping.Services.Product.Application.Interfaces;

namespace Personal.Shopping.Services.Product.Api.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategoriesAsync();

        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult> GetCategoryById(int id)
    {
        var result = await _categoryService.GetCategoryByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
    {
        var result = await _categoryService.CreateCategoryAsync(categoryDto);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(categoryDto);

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);

        return Ok(result);
    }
}
