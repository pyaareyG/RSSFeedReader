using System.Net;
using System.Net.Http.Json;
using RSSFeedReader.UI;

namespace RSSFeedReader.UI.Tests;

public sealed class SubscriptionsClientTests
{
    [Fact]
    public async Task AddsAndLoadsSubscriptionValues()
    {
        var handler = new StubHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost/api/") };
        var client = new SubscriptionsClient(httpClient);

        Assert.True(await client.AddSubscriptionAsync("https://example.com/feed.xml"));
        var subscriptions = await client.GetSubscriptionsAsync();

        Assert.Single(subscriptions);
        Assert.Equal("https://example.com/feed.xml", subscriptions[0].Url);
    }

    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly List<string> urls = [];

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post)
            {
                var body = await request.Content!.ReadFromJsonAsync<SubscriptionRequest>(cancellationToken);
                urls.Add(body!.Url);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new SubscriptionListResponse(
                    urls.Select(url => new SubscriptionItem(url)).ToArray()))
            };
            return response;
        }
    }
}