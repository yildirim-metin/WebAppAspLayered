using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.Models.Books;

namespace WebAppAspLayered.Mappers;

public static class BookMappers
{
    public static BookDto ToBookDto(this Book book)
    {
        return new BookDto()
        {
            ISBN = book.ISBN,
            Name = book.Name,
            Author = book.Author,
        };
    }

    public static List<BookDto> ToBookDtos(this List<Book> books)
    {
        return [.. books.Select(b => b.ToBookDto())];
    }
}
