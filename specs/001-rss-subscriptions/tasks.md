# Tasks: RSS Subscription Management

**Input**: Design documents from `/specs/001-rss-subscriptions/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/

**Tests**: Included because the project constitution requires verification of observable behavior
at API and frontend boundaries.

**Organization**: Tasks are grouped by the single user story so the MVP can be implemented and
validated as one independently useful increment.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the solution and the two application projects described in the implementation
plan.

- [ ] T001 Create the solution structure with `backend/`, `frontend/`, and `tests/` directories per `specs/001-rss-subscriptions/plan.md`
- [ ] T002 Initialize the ASP.NET Core API project in `backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj`
- [ ] T003 [P] Initialize the Blazor WebAssembly frontend project in `frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj`
- [ ] T004 [P] Initialize backend tests in `tests/RSSFeedReader.Api.Tests/RSSFeedReader.Api.Tests.csproj`
- [ ] T005 [P] Initialize frontend tests in `tests/RSSFeedReader.UI.Tests/RSSFeedReader.UI.Tests.csproj`
- [ ] T006 Add all projects to the solution file and verify `dotnet build` succeeds from the repository root

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish shared configuration, routing, error handling, and test conventions before
the user story implementation.

**Checkpoint**: Foundation ready; User Story 1 can now be implemented and tested independently.

- [ ] T007 Remove template demo pages and unused navigation links from `frontend/RSSFeedReader.UI/Pages/` and `frontend/RSSFeedReader.UI/Layout/NavMenu.razor`, leaving one root route for the MVP subscriptions page
- [ ] T008 [P] Configure backend launch ports and frontend launch ports in `backend/RSSFeedReader.Api/Properties/launchSettings.json` and `frontend/RSSFeedReader.UI/Properties/launchSettings.json`
- [ ] T009 [P] Configure the frontend API base URL in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` without hardcoding it in application logic
- [ ] T010 Configure the backend CORS policy in `backend/RSSFeedReader.Api/Program.cs` to allow only the configured local frontend origins
- [ ] T011 Configure API problem-details/error responses and non-sensitive structured logging in `backend/RSSFeedReader.Api/Program.cs`
- [ ] T012 Add shared build/test commands and project assumptions to `README.md`, referencing `specs/001-rss-subscriptions/quickstart.md`

## Phase 3: User Story 1 - Add and View Subscriptions (Priority: P1) MVP

**Goal**: Let one local user submit subscription addresses and immediately see them in the current
session's ordered list.

**Independent Test**: Start the backend and frontend with an empty list, submit two non-empty URLs,
confirm both remain visible in insertion order, submit whitespace-only input, and confirm the list
does not change. Confirm no remote feed request is made.

### Tests for User Story 1

- [ ] T013 [P] [US1] Add subscription service unit tests in `tests/RSSFeedReader.Api.Tests/SubscriptionServiceTests.cs` covering empty initial state, ordered append, duplicate allowance, and whitespace rejection
- [ ] T014 [P] [US1] Add API contract/integration tests in `tests/RSSFeedReader.Api.Tests/SubscriptionsApiTests.cs` covering `GET /api/subscriptions` with `200`, `POST /api/subscriptions` with `201`, and empty/whitespace POST with `400` and no state mutation
- [ ] T015 [P] [US1] Add frontend component tests in `tests/RSSFeedReader.UI.Tests/SubscriptionsTests.razor` covering the URL input, submit interaction, list update, preservation of prior entries, and empty submission behavior

### Implementation for User Story 1

- [ ] T016 [P] [US1] Create the subscription request and response models in `backend/RSSFeedReader.Api/Models/SubscriptionModels.cs` with a required non-whitespace `url` value
- [ ] T017 [US1] Implement the ordered in-memory subscription service in `backend/RSSFeedReader.Api/Services/SubscriptionService.cs`, preserving existing entries, allowing duplicates, and ignoring empty or whitespace-only values
- [ ] T018 [US1] Implement `GET /api/subscriptions` and `POST /api/subscriptions` in `backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs` according to `specs/001-rss-subscriptions/contracts/subscriptions-api.md`; do not make outbound requests
- [ ] T019 [US1] Register the subscription service and controller/API configuration in `backend/RSSFeedReader.Api/Program.cs`
- [ ] T020 [US1] Implement the subscriptions page at `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` with a URL input, submit action, ordered list, and no feed-item rendering
- [ ] T021 [US1] Implement the frontend API client in `frontend/RSSFeedReader.UI/Services/SubscriptionsClient.cs` using the configured API base URL and the documented GET/POST contract
- [ ] T022 [US1] Connect `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` to `SubscriptionsClient.cs`, retaining current-session entries in the displayed list and showing a clear non-blocking empty-input state
- [ ] T023 [US1] Configure the frontend root route and navigation label in `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` and `frontend/RSSFeedReader.UI/Layout/NavMenu.razor`
- [ ] T024 [US1] Run the focused backend and frontend tests, then verify the add/list browser workflow and no-network constraint using `specs/001-rss-subscriptions/quickstart.md`

**Checkpoint**: User Story 1 is independently functional and demonstrates the complete MVP.

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Confirm quality gates and keep the delivered MVP aligned with its documented scope.

- [ ] T025 [P] Review `backend/RSSFeedReader.Api/` and `frontend/RSSFeedReader.UI/` for duplicated business rules, unnecessary dependencies, exposed secrets, and non-sensitive actionable logging
- [ ] T026 [P] Verify `frontend/RSSFeedReader.UI/` has no ambiguous root routes or leftover template demo links
- [ ] T027 Run the complete `dotnet build` and `dotnet test` validation from `specs/001-rss-subscriptions/quickstart.md` and record any deviations in `README.md`
- [ ] T028 Confirm the implementation does not add persistence, feed retrieval/parsing, item rendering, removal, background polling, authentication, or multi-device synchronization to the MVP

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 Setup**: No dependencies; T003-T005 can run in parallel after T001.
- **Phase 2 Foundational**: Depends on T001-T006; T008-T011 can run in parallel after project files exist. Blocks all story work.
- **Phase 3 User Story 1**: Depends on Phase 2. T013-T015 can be prepared in parallel; implementation follows the model/service/endpoint/UI dependency chain.
- **Phase 4 Polish**: Depends on completion of User Story 1 and its focused validation.

### User Story Dependencies

- **User Story 1 (P1)**: Depends only on the foundational phase; it is the complete MVP and has no dependency on later stories.

### Within User Story 1

1. Write the focused tests in T013-T015 against the documented behavior.
2. Define models in T016.
3. Implement the service in T017.
4. Implement and register the API in T018-T019.
5. Implement and connect the frontend in T020-T023.
6. Run the independent validation in T024.

## Parallel Execution Examples

### Setup

```text
Task T003: Initialize frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj
Task T004: Initialize tests/RSSFeedReader.Api.Tests/RSSFeedReader.Api.Tests.csproj
Task T005: Initialize tests/RSSFeedReader.UI.Tests/RSSFeedReader.UI.Tests.csproj
```

### User Story 1

```text
Task T013: Add backend service unit tests
Task T014: Add backend API contract tests
Task T015: Add frontend component tests
```

After T016 is complete, the API and frontend work can proceed in parallel when the shared contract
is stable:

```text
Task T017-T019: Implement backend models, service, and endpoints
Task T020-T023: Implement frontend page, client, and navigation
```

## Implementation Strategy

### MVP First

1. Complete Phase 1 project setup.
2. Complete Phase 2 foundational configuration and route cleanup.
3. Complete Phase 3 User Story 1.
4. Stop and validate the full add/list workflow with `quickstart.md`.
5. Treat the result as the complete MVP; do not begin deferred capabilities in this task set.

### Incremental Delivery

The feature has one user story, so delivery is a single independently testable increment. Future
feed retrieval, item display, persistence, removal, or background updates require a new scoped
specification and plan.

## Notes

- Every task uses the required checkbox, sequential ID, optional `[P]` marker, story label where applicable, and an exact file path.
- `[P]` marks tasks that can work on different files without depending on incomplete work.
- The MVP scope is User Story 1 only.