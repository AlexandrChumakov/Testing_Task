namespace Domain.Shared;

public abstract class Post
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; } 
}