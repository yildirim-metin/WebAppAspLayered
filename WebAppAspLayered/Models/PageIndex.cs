namespace WebAppAspLayered.Models;

public class PageIndex<T> where T : class
{
    public List<T> Items { get; set; } = new List<T>();
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
