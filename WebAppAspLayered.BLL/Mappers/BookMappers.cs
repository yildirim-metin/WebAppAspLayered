using WebAppAspLayered.BLL.Models;
using WebAppAspLayered.DAL.Models;

namespace WebAppAspLayered.BLL.Mappers;

public static class BookMappers
{
    public static BookFilterDal ToBookFilterDal(this BookFilterBll bookFilterBll)
    {
        return new()
        {
            ISBN = bookFilterBll.ISBN,
            Name = bookFilterBll.Name,
        };
    }
}
