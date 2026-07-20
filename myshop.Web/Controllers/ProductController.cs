using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.DAL.Context;
using myshop.DAL.Interfaces;
using myshop.Web.Services.IServices;
using myshop.Web.ViewModels.Product;

namespace myshop.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var products = await _productService.GetAllAsync();

            return Json(new { data = products });
        }

        [HttpGet]
        [Authorize(policy: "AdminOnly")]
        public IActionResult Create()
        {
            ProductCreateVM productVM = new ProductCreateVM()
            {
                Categories = _categoryService.GetAllAsync()
                .Result.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<ProductCreateDto>(productVM);
                
                if (productVM.ImgPath is not null)
                    product.ImgPath = await _imageService.SaveImageAsync(productVM.ImgPath, "Products"); ;
                
                await _productService.AddAsync(product);
                
                TempData["Create"] = "Item has Created Successfully";
                
                return RedirectToAction("Index");
            }
            return View(productVM);
        }
        
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            var updateProduct = _mapper.Map<ProductUpdateVM>(product);
            updateProduct.CategoryList = (await _categoryService.GetAllAsync())
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            return View(updateProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateVM productVM)
        {
            if (!ModelState.IsValid)
            {
                productVM.CategoryList = (await _categoryService.GetAllAsync())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });

                return View(productVM);
            }
            
            var productInDb = await _productService.GetByIdAsync(productVM.Id);
            if(productInDb is null)
            {
                return NotFound("ياحرامي يابن ....");
            }

            var dto = _mapper.Map<ProductUpdateDto>(productVM);
            
            if(productVM.Img is not null)
                dto.ImgPath = await _imageService.ReplaceImageAsync(productVM.Img, productInDb.ImgPath, "Products");
            
            await _productService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product is null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting."
                });
            }

            _imageService.DeleteImage(product.ImgPath);

            await _productService.Delete(id);

            return Json(new
            {
                success = true,
                message = "Product deleted successfully."
            });
        }

    }
}
