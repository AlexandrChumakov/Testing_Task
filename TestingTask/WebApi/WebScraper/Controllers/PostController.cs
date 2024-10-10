using Application.WebScraper.Interfaces;
using Domain.WebScraper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestingTask.WebApi.WebScraper.Controllers;

[Authorize]
[ApiController]
[Route("api/")]
public class PostController(IPostRepository repository) : ControllerBase
{
    [Route("topten")]
    [HttpGet]
    public async Task<ActionResult> TopTenAsync()
    {
        return Ok(await repository.TakeTopTenAsync());
    }

    [Route("posts")]
    [HttpGet]
    public async Task<ActionResult<List<Post>>> TopTenAsync([FromQuery(Name = "from")] DateTime from,
        [FromQuery(Name = "to")] DateTime to)
    {
        return Ok(await repository.GetSortedAsync(from, to));
    }

    [Route("search")]
    [HttpGet]
    public async Task<ActionResult> SearchAsync([FromQuery(Name = "text")] string value)
    {
        return Ok(await repository.GetContainsAsync(value));
    }
}