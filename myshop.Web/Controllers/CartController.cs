using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Cart;
using myshop.BLL.Interfaces;
using myshop.Web.ViewModels;

namespace myshop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }
        
        public IActionResult Index()
        {
            return View(new CartVM()
            {
                Items = _cartService.GetCart(),
                OrderTotal = _cartService.GetOrderTotal(),
                TotalItems = _cartService.GetTotalItems()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int productId, string? returnUrl)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product is null)
                return NotFound();
            
            _cartService.AddItem(new CartItemDto
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                ImageUrl = product.ImgPath
            });

            TempData["Success"] = $"{product.Name} added to cart.";

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int productId)
        {
            _cartService.RemoveItem(productId);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Increase(int productId)
        {
            _cartService.IncreaseQuantity(productId);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Decrease(int productId)
        {
            _cartService.DecreaseQuantity(productId);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Clear()
        {
            _cartService.ClearCart();

            return RedirectToAction(nameof(Index));
        }

    }
}
