# Research: RSS Subscription Management

## Decision: Use an in-memory backend subscription service

**Rationale**: The MVP explicitly requires session-only data and excludes persistence. A small
service owning an ordered collection is sufficient for add/list behavior and leaves a clear seam
for a later persistence decision.

**Alternatives considered**: Browser-only state would bypass the documented API/frontend boundary;
database storage would add scope and operational complexity without MVP value.

## Decision: Expose separate add and list API operations

**Rationale**: The frontend needs a clear contract for submitting one subscription and loading the
current list. Separate operations keep the contract explicit and make each behavior independently
testable.

**Alternatives considered**: A combined replace-list operation was rejected because it creates a
larger mutation surface than the MVP needs.

## Decision: Treat URLs as display data in the MVP

**Rationale**: The requirements explicitly defer URL validation, feed fetching, and parsing. The
API will reject empty values but will not make outbound requests or inspect remote content.

**Alternatives considered**: URL and feed validation was rejected as Extended-MVP behavior and
would introduce network and security concerns before they are needed.

## Decision: Verify both the API boundary and browser workflow

**Rationale**: The constitution requires tests at the risk boundary. Automated tests cover the
subscription behavior and API contract, while the quickstart covers ports, CORS, routing cleanup,
and the end-to-end add/list smoke test.

**Alternatives considered**: Backend-only testing was rejected because the user value is delivered
through the browser workflow and cross-origin configuration.