using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Web.ViewModels.Product;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(PagedRequestDto requestDto)
        {
            var pagedProducts = await _productService.GetPagedAsync(requestDto);

            return Json(new
            {
                draw = Request.Query["draw"],
                data = pagedProducts.Data,
                recordsTotal = pagedProducts.TotalCount,
                recordsFiltered = pagedProducts.FilteredCount
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductCreateVM productVM = new ProductCreateVM()
            {
                CategoryList = (await _categoryService.GetAllAsync())
                    .Select(x => new SelectListItem
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

            var product = _mapper.Map<ProductCreateDto>(productVM);

            if (productVM.ImgFile is not null)
            {
                product.ImageFileName = productVM.ImgFile.FileName;
                product.ImageSize = productVM.ImgFile.Length;
                product.ImageContent = productVM.ImgFile.OpenReadStream();
            }
            try
            {
                await _productService.AddAsync(product);
            }
            catch(ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                productVM.CategoryList = (await _categoryService.GetAllAsync())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
                return View(productVM);
            }
                
            TempData["Create"] = "Item has Created Successfully";
                
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product is null)
                return NotFound();
            
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
            
            // Update is already getting Product
            
            //var productInDb = await _productService.GetByIdAsync(productVM.Id);
            
            //if(productInDb is null)
            //{
            //    return NotFound("ياحرامي يابن ....");
            //}

            var dto = _mapper.Map<ProductUpdateDto>(productVM);

            if (productVM.Img is not null)
            {
                dto.ImageFileName = productVM.Img.FileName;
                dto.ImageSize = productVM.Img.Length;
                dto.ImageContent = productVM.Img.OpenReadStream();
            }

            try
            {
                await _productService.Update(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return Json(new
                {
                    success = false,
                    message = "Product not found."
                });
            }
            return Json(new
            {
                success = true,
                message = "Product deleted successfully."
            });
        }

    }
}
