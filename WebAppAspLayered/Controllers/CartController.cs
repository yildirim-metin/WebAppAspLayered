using Microsoft.AspNetCore.Mvc;
using WebAppAspLayered.BLL.Services;
using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.Extensions;
using WebAppAspLayered.Models.Carts;

namespace WebAppAspLayered.Controllers;

public class CartController : Controller
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    public IActionResult AddItem([FromQuery] int bookId)
    {
        if (!User.IsConnected())
        {
            var cartSession = HttpContext.Session.GetItem<List<CartItemSessionDto>>("cart") ?? [];

            CartItemSessionDto? item = cartSession.FirstOrDefault(p => p.ProductId == bookId);

            if (item == null)
            {
                cartSession.Add(new CartItemSessionDto()
                {
                    ProductId = bookId,
                    Quantity = 1,
                });
            }
            else
            {
                item.Quantity++;
            }

            HttpContext.Session.SetItem("cart", cartSession);
        }
        else
        {
            Cart cart = _cartService.GetCartByUserId(User.GetId());

            CartItem? item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            
            if (item is null)
            {
                item = new()
                {
                    CartId = cart.Id,
                    BookId = bookId,
                    Quantity = 1,
                };
            }
            else
            {
                item.Quantity++;
            }

            _cartService.AddItem(item);
        }

        return RedirectToAction("Index", "Book");
    }
}
