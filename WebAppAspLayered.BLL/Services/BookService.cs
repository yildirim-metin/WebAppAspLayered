using WebAppAspLayered.BLL.Mappers;
using WebAppAspLayered.BLL.Models;
using WebAppAspLayered.DAL.Repositories;
using WebAppAspLayered.DL.Entities;

namespace WebAppAspLayered.BLL.Services;

public class BookService
{
    private readonly BookRepository _repository;

    public BookService(BookRepository repository)
    {
        _repository = repository;
    }

    public List<Book> GetAll(int page, BookFilterBll? filter)
    {
        return _repository.GetAll(page, filter?.ToBookFilterDal());
    }

    public int Count()
    {
        return _repository.Count();
    }
}
