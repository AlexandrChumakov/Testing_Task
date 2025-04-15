using Domain.Shared;

namespace Domain.WebScraper.Models;

public class WebPost : Post
{
    public override string ToString()
    {
        return
            $"<b> {Title} </b> \n \n  <b> Описание: </b> <i> {Description} </i>   \n \n <b> Дата публикации: </b> {Date.Date}";
    }
}