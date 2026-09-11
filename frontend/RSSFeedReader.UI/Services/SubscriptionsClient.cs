using System.Net.Http.Json;

namespace RSSFeedReader.UI;

public sealed class SubscriptionsClient(HttpClient httpClient)
{
    public async Task<IReadOnlyList<SubscriptionItem>> GetSubscriptionsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<SubscriptionListResponse>("subscriptions");
        return response?.Subscriptions ?? [];
    }

    public async Task<bool> AddSubscriptionAsync(string url)
    {
        using var response = await httpClient.PostAsJsonAsync("subscriptions", new SubscriptionRequest(url));
        return response.IsSuccessStatusCode;
    }
}

public sealed record SubscriptionRequest(string Url);

public sealed record SubscriptionListResponse(IReadOnlyList<SubscriptionItem> Subscriptions);

public sealed record SubscriptionItem(string Url);