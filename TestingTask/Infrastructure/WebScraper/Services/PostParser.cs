using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Application.WebScraper.Interfaces;
using Domain.WebScraper.Models;

namespace Infrastructure.WebScraper.Services;

public partial class PostParser : IPostParser
{
    private const string Pattern = @"<div\s+class=""content""[^>]*>(.*?)<\/div>";
    private const string Url = "https://www.zakon.kz/";
    private readonly List<JsonNode> _jsonNodes = [];
    private readonly List<Post> _posts = [];

    public async Task<List<Post>> ParsePosts()
    {
        var client = new HttpClient();

        using var pageOne = await client.GetAsync($"{Url}api/today-news/?pn=1&pSize=50");
        pageOne.EnsureSuccessStatusCode();
        await AddListPosts(pageOne);

        using var pageTwo = await client.GetAsync($"{Url}api/today-news/?pn=3&pSize=50");
        pageTwo.EnsureSuccessStatusCode();
        await AddListPosts(pageTwo);

        await ParseHtml(client);

        return _posts;
    }

    private async Task AddListPosts(HttpResponseMessage responseMessage)
    {
        var responseBody = await responseMessage.Content.ReadAsStringAsync();
        var json = JsonNode.Parse(responseBody)!.AsObject();
        _jsonNodes.AddRange(json["data_list"]!.AsArray().ToList()!);
    }

    private async Task ParseHtml(HttpClient client)
    {
        foreach (var item in _jsonNodes)
        {
            var title = item["page_title"]!.GetValue<string>();
            var decLink = item["alias"]!.GetValue<string>();
            var time = item["published_date"]!.GetValue<string>();

            using var httpResponseMessage = await client.GetAsync(Url + decLink);
            httpResponseMessage.EnsureSuccessStatusCode();

            var html = await httpResponseMessage.Content.ReadAsStringAsync();
            var match = MyRegex().Match(html);
            var innerHtml = match.Value;
            var desc = MyRegex1().Replace(innerHtml, string.Empty);
            _posts.Add(new Post
            {
                Date = DateTime.Parse(time),
                Description = desc.Trim(),
                Title = title
            });
        }
    }

    [GeneratedRegex(Pattern, RegexOptions.Singleline)]
    private static partial Regex MyRegex();

    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex1();
}