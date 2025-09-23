namespace WebAppAspLayered.DL.Entities;

public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
}
