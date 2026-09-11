using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Tests;

public sealed class SubscriptionsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public SubscriptionsApiTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task CanAddAndListSubscriptions()
    {
        var addResponse = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new SubscriptionRequest("https://example.com/feed.xml"));

        var listResponse = await client.GetFromJsonAsync<SubscriptionListResponse>("/api/subscriptions");

        Assert.Equal(HttpStatusCode.Created, addResponse.StatusCode);
        Assert.NotNull(listResponse);
        Assert.Contains(listResponse.Subscriptions, item => item.Url == "https://example.com/feed.xml");
    }

    [Fact]
    public async Task RejectsEmptySubscriptionWithoutAddingIt()
    {
        var response = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new SubscriptionRequest("   "));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}