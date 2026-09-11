# Data Model: RSS Subscription Management

## Subscription

Represents one feed address entered by the local user during the current application session.

### Fields

| Field | Type | Required | Rules |
|---|---|---:|---|
| `url` | string | Yes | Must contain non-whitespace text; MVP does not verify feed validity or fetch it. |

### Lifecycle

1. A non-empty submitted value becomes a subscription.
2. The subscription is appended to the current session's ordered list.
3. The subscription remains available through list requests until the backend process ends.
4. The subscription is discarded when the session/backend process ends.

## Subscription List

An ordered collection of `Subscription` values owned by the running application session.

### Rules

- Adding a new subscription preserves all existing entries.
- Duplicate values MAY appear; de-duplication is out of scope.
- Empty or whitespace-only submissions do not change the collection.
- The collection is local to the running application and is not shared or persisted.