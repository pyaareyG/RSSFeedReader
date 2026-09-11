namespace RSSFeedReader.Api.Models;

public sealed record SubscriptionRequest(string? Url);

public sealed record SubscriptionResponse(string Url);

public sealed record SubscriptionListResponse(IReadOnlyList<SubscriptionResponse> Subscriptions);