using System.Text.Json;

namespace Domain.WebScraper.Models;

public struct Post
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }


    public override string ToString()
    {
        return
            $"<b> {Title} </b> \n \n  <b> Описание: </b> <i> {Description} </i>   \n \n <b> Дата публикации: </b> {Date.Date}";
    }
}