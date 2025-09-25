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

    public List<Book> GetAll(int page)
    {
        return _repository.GetAll(page);
    }

    public int Count()
    {
        return _repository.Count();
    }
}
