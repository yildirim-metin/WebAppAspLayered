using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using WebAppAspLayered.BLL.Services;
using WebAppAspLayered.Mappers;
using WebAppAspLayered.Models.Books;
using WebAppAspLayered.Models.Pagination;

namespace WebAppAspLayered.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService service)
    {
        _bookService = service;
    }

    public IActionResult Index(int page = 0, [FromQuery]BookFilterFormDto? filter = null)
    {
        List<BookDto> books = _bookService.GetAll(page, filter?.ToBookFilterBll()).ToBookDtos();
        PageIndex<BookDto> p = new()
        {
            Items = books,
            Meta = new()
            {
                ItemsCount = books.Count,
                Page = page,
                TotalItems = _bookService.Count(),
                TotalPages = (int)Math.Ceiling(_bookService.Count() / 5f) - 1,
            }
        };
        return View((p, filter));
    }

    [HttpPost]
    public IActionResult AddFavorite()
    {

        return View();
    }
}
