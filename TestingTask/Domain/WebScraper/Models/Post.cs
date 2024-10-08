namespace Domain.WebScraper.Models;

public struct Post
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Date { get; set; }
}