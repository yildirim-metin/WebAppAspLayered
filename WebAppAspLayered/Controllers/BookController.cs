using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAspLayered.BLL.Services;
using WebAppAspLayered.Mappers;
using WebAppAspLayered.Models.Books;

namespace WebAppAspLayered.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService service)
    {
        _bookService = service;
    }

    public IActionResult Index()
    {
        return View(_bookService.GetAll().ToBookDtos());
    }

    [HttpPost]
    public IActionResult AddFavorite()
    {

        return View();
    }
}
