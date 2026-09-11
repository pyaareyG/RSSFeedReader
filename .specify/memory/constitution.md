<!--
Sync Impact Report
- Version change: unratified scaffold -> 1.0.0
- Modified principles: placeholder principles -> five project-specific core principles
- Added sections: Technology and Product Constraints; Development Workflow and Quality Gates
- Removed sections: none
- Follow-up TODOs: confirm the original ratification date
-->

# RSS Feed Reader Constitution

## Core Principles

### I. Security by Design
All input crossing the frontend, API, or future feed-fetching boundary MUST be treated as
untrusted. API endpoints MUST validate request shape and enforce appropriate limits; outbound
feed requests MUST use approved schemes, timeouts, and redirect controls; rendered feed content
MUST be sanitized before HTML is introduced. Secrets MUST remain outside source control and
client-delivered configuration. Security-sensitive behavior MUST have a regression test or an
explicit documented reason why a test is not practical.

### II. Focused Incremental Delivery
Each change MUST have a clear MVP, Extended-MVP, or post-MVP purpose and MUST avoid implementing
deferred functionality without an approved requirement. The MVP MUST remain limited to adding a
subscription URL and displaying the in-memory subscription list. New abstractions, dependencies,
and operational complexity MUST be justified by a current requirement or a documented extension
path.

### III. Clear Layered Contracts
The ASP.NET Core API MUST own data and feed operations, while the Blazor WebAssembly frontend
MUST own presentation and user interaction. Communication between layers MUST use explicit,
documented request and response models rather than leaking implementation details. Configuration,
including API base URLs and CORS origins, MUST be externalized and kept consistent across local
development settings.

### IV. Verification at the Risk Boundary
Every feature MUST include tests for its observable behavior. API changes MUST include endpoint
or integration coverage, frontend behavior MUST include component or UI coverage where practical,
and cross-layer changes MUST verify the API contract and CORS/configuration path. A change MUST
not be considered complete while the affected projects fail to build, configured tests fail, or
the documented local smoke test is broken.

### V. Maintainable, Observable Code
Code MUST use the established ASP.NET Core and Blazor patterns, favor small cohesive units, and
avoid duplicated business rules. Failures at runtime MUST produce actionable structured logs on
the backend without exposing secrets or unnecessary personal data. Public API behavior and
non-obvious design decisions MUST be documented near the owning code or in project documentation.
Refactoring MUST preserve tested behavior unless the change explicitly updates the contract.

## Technology and Product Constraints

The project uses an ASP.NET Core Web API backend and a Blazor WebAssembly frontend. The MVP uses
in-memory subscription storage and does not fetch, parse, or render feed content. Extended-MVP
feed operations use `System.ServiceModel.Syndication` and manual refresh only. CORS MUST allow
only the configured frontend origins, and local API configuration MUST match the backend launch
settings. Any future persistence, background polling, HTML rendering, or multi-device capability
requires a separately reviewed feature scope and corresponding security and data-lifecycle
decisions.

## Development Workflow and Quality Gates

Before implementation, work MUST identify the affected layer, user-visible behavior, security
implications, and verification strategy. Before review, contributors MUST run the applicable
build and test commands, verify that no template routes conflict, and perform the documented
local smoke test for the changed workflow. Reviews MUST check scope compliance, input and output
validation, secret handling, error behavior, and maintainability. Exceptions MUST be recorded
with their rationale and an owner or follow-up milestone.

## Governance

This constitution governs project requirements and supersedes conflicting informal practices.
Amendments MUST state the motivation, affected principles or sections, compatibility impact, and
required follow-up work. Versioning follows semantic rules: MAJOR for incompatible governance
changes or principle removals, MINOR for new or materially expanded principles, and PATCH for
clarifications that do not change obligations. Every implementation review MUST verify compliance
with the principles and quality gates above; exceptions require explicit documentation. The
constitution MUST be reviewed whenever the product scope, architecture, or security boundary
changes.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): confirm original adoption date | **Last Amended**: 2026-09-11
