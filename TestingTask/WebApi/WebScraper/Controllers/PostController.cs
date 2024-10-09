using Application.WebScraper.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace TestingTask.WebApi.WebScraper.Controllers;

[ApiController]
[Route("api/")]
public class PostController(IPostService postService) : ControllerBase
{
    [Route("topten")]
    [HttpGet]
    public async Task<ActionResult> TopTenAsync()
    {
        return Ok(await postService.TakeTopTenAsync());
    }
    
    [Route("posts")]
    [HttpGet]
    public async Task<ActionResult> TopTenAsync([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to)
    {
        return Ok(await postService.FilteredPostsAsync(from, to));
    }
    
}