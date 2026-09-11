using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public sealed class SubscriptionService
{
    private readonly List<string> subscriptions = [];

    public IReadOnlyList<SubscriptionResponse> GetAll()
    {
        return subscriptions.Select(url => new SubscriptionResponse(url)).ToArray();
    }

    public SubscriptionResponse? Add(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return null;
        }

        var normalizedUrl = url.Trim();
        subscriptions.Add(normalizedUrl);
        return new SubscriptionResponse(normalizedUrl);
    }
}