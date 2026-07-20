using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Category;
using myshop.BLL.Interfaces;
using myshop.Web.ViewModels.Category;

namespace myshop.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var categories = await _categoryService.GetAllAsync();

            return Json(new { data = categories });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM categoryVM)
        {
            if (!ModelState.IsValid)
                return View(categoryVM);

            var dto = _mapper.Map<CategoryCreateDto>(categoryVM);

            await _categoryService.AddAsync(dto);

            TempData["Create"] = "Item has Created Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            var vm = _mapper.Map<CategoryUpdateVM>(category);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryUpdateVM categoryVM)
        {
            if (!ModelState.IsValid)
                return View(categoryVM);

            var dto = _mapper.Map<CategoryUpdateDto>(categoryVM);

            await _categoryService.Update(dto);

            TempData["Update"] = "Data has Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting."
                });
            }

            await _categoryService.Delete(id);

            return Json(new
            {
                success = true,
                message = "Category deleted successfully."
            });
        }
    }
}