using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Checkout;
using myshop.BLL.Interfaces;
using myshop.Web.ViewModels.Checkout;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IMapper _mapper;
        public CheckoutController(ICheckoutService checkoutService, IMapper mapper)
        {
            _checkoutService = checkoutService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutVM checkoutVM)
        {
            if (!ModelState.IsValid)
                return View(checkoutVM);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var dto = _mapper.Map<CheckoutDto>(checkoutVM);

            try
            {
                var orderId = await _checkoutService.CreateOrderAsync(dto, userId);
                
                TempData["Success"] = $"Order #{orderId} has been placed successfully.";

                return RedirectToAction(nameof(Index), nameof(OrderController));
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return View(checkoutVM);
            }
        }
    }
}
