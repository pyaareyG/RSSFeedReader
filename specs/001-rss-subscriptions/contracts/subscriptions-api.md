# Subscription API Contract

Base path: `/api/subscriptions`

The API manages subscription addresses only. It MUST NOT fetch or parse the referenced feeds for
this MVP.

## List subscriptions

`GET /api/subscriptions`

### Success response

- Status: `200 OK`
- Body:

```json
{
  "subscriptions": [
    { "url": "https://example.com/feed.xml" }
  ]
}
```

The array preserves insertion order. An empty list is valid.

## Add subscription

`POST /api/subscriptions`

### Request body

```json
{
  "url": "https://example.com/feed.xml"
}
```

The `url` value MUST contain non-whitespace text. The MVP does not validate whether it is a
working RSS or Atom feed.

### Success response

- Status: `201 Created`
- Body:

```json
{
  "url": "https://example.com/feed.xml"
}
```

### Empty request response

- Status: `400 Bad Request`
- Body:

```json
{
  "title": "Subscription URL is required"
}
```

The server MUST NOT add an entry when the request is empty or whitespace-only.

## Cross-origin behavior

The API MUST allow requests only from the configured local frontend origins. The frontend API
base URL MUST be read from its local configuration and MUST match the backend launch settings.