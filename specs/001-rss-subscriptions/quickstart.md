# Quickstart: RSS Subscription Management

## Prerequisites

- A supported .NET SDK installed.
- A supported desktop browser.
- The backend and frontend projects created at the paths in [plan.md](plan.md).

## Configuration checks

1. Confirm the backend listens on `http://localhost:5151` (or record the configured alternative).
2. Confirm the frontend listens on `http://localhost:5213` (or record the configured alternative).
3. Confirm `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` points to the backend API base URL.
4. Confirm backend CORS allows the exact frontend HTTP/HTTPS origins.
5. **Route cleanup**: Confirm only the subscriptions page owns the root route `/`; remove template
   demo pages and navigation links that are not part of the MVP.

## Automated validation

From the repository root:

```powershell
dotnet build
dotnet test
```

Expected result: all affected projects build and all configured tests pass.

## API contract checks

With the backend running, verify:

```powershell
Invoke-RestMethod -Method Get -Uri http://localhost:5151/api/subscriptions
Invoke-RestMethod -Method Post -Uri http://localhost:5151/api/subscriptions -ContentType 'application/json' -Body '{"url":"https://example.com/feed.xml"}'
Invoke-RestMethod -Method Get -Uri http://localhost:5151/api/subscriptions
```

Expected result: the first list is empty, the POST succeeds, and the second list contains the
submitted URL. A whitespace-only POST returns `400 Bad Request` and does not add an entry.

## Browser smoke test

1. Start the backend and frontend.
2. Open the frontend URL in the browser.
3. Enter `https://example.com/feed.xml` and submit it.
4. Confirm the URL appears in the list immediately.
5. Add a second URL and confirm the first remains visible.
6. Confirm browser developer tools show no connection or CORS errors.
7. Confirm no feed request or feed item is displayed; the MVP only manages addresses.