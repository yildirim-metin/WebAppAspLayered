namespace WebAppAspLayered.Models.Books;

public record BookDto
{
    public int Id { get; set; }
    public string ISBN { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsFav { get; set; }
}
