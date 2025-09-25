namespace WebAppAspLayered.Models.Books;

public record BookFilterFormDto
{
    public string? ISBN { get; set; }
    public string? Name { get; set; }
}
