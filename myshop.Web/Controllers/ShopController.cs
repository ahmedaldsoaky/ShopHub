using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Product;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Entities.Models;
using myshop.Web.ViewModels.Shop;

namespace myshop.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(
                            PagedRequestDto request)
        {
            request.PageSize = 12;

            request.SortColumn ??= "Id";
            request.SortDirection ??= "desc";


            var products = await _productService.GetPagedAsync(request);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            ProductDetailsVM detailsVM = new ProductDetailsVM()
            {
                Product = product,
                RelatedProducts = await _productService
                    .GetRelatedProductsAsync(
                        product.CategoryId,
                        product.Id,
                        4)
            };

            return View(detailsVM);
        }

    }
}
