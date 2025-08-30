using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Product;
using Personal.Shopping.Manager.Web.Services.Interfaces;

namespace Personal.Shopping.Manager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiClient _categoryService;

        public CategoryController(ICategoryApiClient categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryIndex()
        {
            List<CategoryDto> list = new();
            
            ResponseDto? response = await _categoryService.GetAllCategoriesAsync();

            if (response is not null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result!)!)!;
            }

            return View(list);
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _categoryService.CreateCategoryAsync(model);

                if (response is not null && response.IsSuccess)
                {
                    TempData["Message"] = "Produto criado com sucesso!";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("CategoryIndex");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CategoryEdit(int id)
        {
            CategoryDto categoryDto = new();

            ResponseDto? response = await _categoryService.GetCategoryByIdAsync(id);

            if (response is not null && response.IsSuccess)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result!)!)!;

                return View(categoryDto);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _categoryService.UpdateCategoryAsync(model);
            return RedirectToAction(nameof(CategoryIndex));
        }

        public async Task<IActionResult> CategoryDelete(int id)
        {
            ResponseDto? response = await _categoryService.DeleteCategoryAsync(id);

            if (response.IsSuccess)
            {
                TempData["Message"] = "Produto excluído com sucesso!";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "Erro ao excluir produto!";
                TempData["MessageType"] = "error";
            }

            return RedirectToAction(nameof(CategoryIndex));
        }
    }
}
