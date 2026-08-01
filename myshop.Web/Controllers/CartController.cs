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
            var model = new CartViewModel(){
                Items = _cartService.GetCart(),
                OrderTotal = _cartService.GetOrderTotal()
            };
            return View(model);
        }

        public async Task<IActionResult> Add(int productId)
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            if (product is null)
                return NotFound();
            _cartService.RemoveItem(productId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Increase(int id)
        {
            _cartService.IncreaseQuantity(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrease(int id)
        {
            _cartService.DecreaseQuantity(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartService.ClearCart();

            return RedirectToAction(nameof(Index));
        }

    }
}
