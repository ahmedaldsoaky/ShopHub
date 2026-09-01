using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;

namespace myshop.Web.ViewComponents;

public class CartBadgeViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartBadgeViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        return View(_cartService.GetItemCount());
    }
}