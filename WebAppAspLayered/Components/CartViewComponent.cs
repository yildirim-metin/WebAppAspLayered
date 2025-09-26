using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppAspLayered.BLL.Services;
using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.Extensions;
using WebAppAspLayered.Models.Carts;

namespace WebAppAspLayered.Components;

public class CartViewComponent : ViewComponent
{
    private readonly CartService _cartService;

    public CartViewComponent(CartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        int quantity = 0;

        if (!User.IsConnected())
        {
            var cart = HttpContext.Session.GetItem<List<CartItemSessionDto>>("cart") ?? [];
            quantity = cart.Sum(e => e.Quantity);
        }
        else
        {
            if (User is ClaimsPrincipal claims)
            {
                Cart cart = _cartService.GetCartByUserId(claims.GetId());
                quantity = cart.Items.Sum(e => e.Quantity);
            }
        }

        return View(quantity);
    }
}
