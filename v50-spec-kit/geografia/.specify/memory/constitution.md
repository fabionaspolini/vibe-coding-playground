# Geografia API Constitution

## Core Principles

### I. Entity-Driven CRUD
All features must support full Create, Read, Update, and Delete (CRUD) operations for the core entities: País, Estado, and Cidade. Each entity must be independently manageable via the API, with clear, RESTful endpoints and strong validation.

### II. Event-Driven Architecture
Every CRUD action performed via the API MUST emit a corresponding event to a Kafka topic. Events must be well-structured, entity-specific, and include sufficient context for real-time consumers to react reliably. Event emission is mandatory for all data mutations and queries that alter state.

### III. Consistency and Atomicity
All operations that modify data MUST be atomic and consistent. Either all changes (including event emission) succeed, or none do. The system must prevent partial updates or event loss.

### IV. Observability and Traceability
All API actions and Kafka events MUST be logged with traceable identifiers. Structured logging is required for all operations, and correlation IDs must be propagated from API to event.

### V. Simplicity and Extensibility
The API and event schemas must be simple, well-documented, and designed for future extensibility (e.g., adding new geographic entities or event types without breaking existing consumers).

## Additional Constraints

- Technology stack: .NET (latest LTS), Kafka (latest stable), RESTful API design
- All endpoints must be versioned (e.g., /v1/pais)
- API must be documented via OpenAPI/Swagger
- Security: All endpoints require authentication and authorization
- Performance: CRUD operations must complete within 200ms p95 under expected load
- Compliance: All data must conform to geographic standards (e.g., ISO country/state codes)

## Development Workflow

- All code changes require code review and must pass automated tests (unit + integration)
- CI/CD pipeline must verify event emission and API contract compliance
- Breaking changes require a migration plan and explicit version bump
- Documentation must be updated with every change

## Governance

- This constitution supersedes all other development practices for this project
- Amendments require documentation, team approval, and a migration plan if breaking
- All PRs/reviews must verify compliance with these principles and constraints
- Complexity must be justified in design docs
- Use SPEC_KIT_SETUP_INICIAL.md for runtime development guidance

**Version**: 1.0.0 | **Ratified**: 2026-03-30 | **Last Amended**: 2026-03-30

<!--
Sync Impact Report:
- Version change: N/A → 1.0.0
- Modified principles: N/A (initial version)
- Added sections: All
- Removed sections: None
- Templates requiring updates: ✅ plan-template.md, ✅ spec-template.md, ✅ tasks-template.md, ✅ checklist-template.md, ✅ agent-file-template.md
- Follow-up TODOs: None
-->
