using Microsoft.AspNetCore.Mvc;
using WebAppAspLayered.Extensions;
using WebAppAspLayered.Models.Carts;

namespace WebAppAspLayered.Components;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        int quantity = 0;

        if (!User.IsConnected())
        {
            var cart = HttpContext.Session.GetItem<List<CartItemSessionDto>>("cart") ?? [];
            quantity = cart.Sum(e => e.Quantity);
        }

        return View(quantity);
    }
}
