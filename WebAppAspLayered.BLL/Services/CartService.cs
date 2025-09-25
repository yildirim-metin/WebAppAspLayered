using WebAppAspLayered.DAL.Repositories;
using WebAppAspLayered.DL.Entities;

namespace WebAppAspLayered.BLL.Services;

public class CartService
{
    private readonly CartRepository _repository;

    public CartService(CartRepository repository)
    {
        _repository = repository;
    }

    public Cart GetCartByUserId(int userId)
    {
        return _repository.GetCartByUserId(userId);
    }
}
