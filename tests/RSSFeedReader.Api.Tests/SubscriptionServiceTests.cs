using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Tests;

public sealed class SubscriptionServiceTests
{
    [Fact]
    public void StartsWithAnEmptyList()
    {
        var service = new SubscriptionService();

        Assert.Empty(service.GetAll());
    }

    [Fact]
    public void AppendsSubscriptionsInOrder()
    {
        var service = new SubscriptionService();

        service.Add("https://example.com/first.xml");
        service.Add("https://example.com/second.xml");

        Assert.Equal(
            ["https://example.com/first.xml", "https://example.com/second.xml"],
            service.GetAll().Select(subscription => subscription.Url));
    }

    [Fact]
    public void AllowsDuplicateSubscriptions()
    {
        var service = new SubscriptionService();

        service.Add("https://example.com/feed.xml");
        service.Add("https://example.com/feed.xml");

        Assert.Equal(2, service.GetAll().Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void RejectsEmptyOrWhitespaceValues(string? url)
    {
        var service = new SubscriptionService();

        Assert.Null(service.Add(url));
        Assert.Empty(service.GetAll());
    }
}