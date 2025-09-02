using Personal.Shopping.OrderManager.Web.Models;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface ICategoryApiClient
{
    [Get("/api/categories")]
    Task<ResponseDto> GetAllCategoriesAsync();

    [Get("/api/categories/get-by-id/{id}")]
    Task<ResponseDto> GetCategoryByIdAsync(int id);
}
