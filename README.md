# RSS Feed Reader

This local proof of concept demonstrates adding RSS/Atom subscription URLs and displaying them
for the current application session.

## Run locally

Start the API and frontend from separate terminals:

```powershell
dotnet run --project backend/RSSFeedReader.Api
dotnet run --project frontend/RSSFeedReader.UI
```

The API uses `http://localhost:5151`; the frontend uses `http://localhost:5213`.

## Validate

```powershell
dotnet build
dotnet test
```

See [specs/001-rss-subscriptions/quickstart.md](specs/001-rss-subscriptions/quickstart.md) for
the API and browser smoke checks. Feed retrieval, parsing, item display, persistence, removal,
and background polling are outside this MVP.