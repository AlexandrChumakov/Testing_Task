using Domain.Shared;

namespace Domain.TgBot.Models;

public class TgPost : Post
{
  public override string ToString()
  {
    return
      $"<b> {Title} </b> \n \n  <b> Описание: </b> <i> {Description} </i>   \n \n <b> Дата публикации: </b> {Date:dd.MM.yyyy}";
  }
}