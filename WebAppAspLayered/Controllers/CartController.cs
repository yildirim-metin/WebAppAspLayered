using Microsoft.AspNetCore.Mvc;
using WebAppAspLayered.Extensions;
using WebAppAspLayered.Models.Carts;

namespace WebAppAspLayered.Controllers
{
    public class CartController : Controller
    {
        public IActionResult AddItem([FromQuery] int productId)
        {
            if (!User.IsConnected())
            {
                var cartSession = HttpContext.Session.GetItem<List<CartItemSessionDto>>("cart") ?? [];

                CartItemSessionDto? item = cartSession.FirstOrDefault(p => p.ProductId == productId);

                if (item == null)
                {
                    cartSession.Add(new CartItemSessionDto()
                    {
                        ProductId = productId,
                        Quantity = 1,
                    });
                }
                else
                {
                    item.Quantity++;
                }

                HttpContext.Session.SetItem("cart", cartSession);
            }

            return RedirectToAction("Index", "Book");
        }
    }
}
