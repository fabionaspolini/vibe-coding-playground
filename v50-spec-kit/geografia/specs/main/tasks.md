---
description: "Task list for Geografia CRUD API implementation"
---

# Tasks: Geografia CRUD API

**Input**: Design documents from `/specs/main/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

## Phase 1: Setup (Shared Infrastructure)

- [ ] T001 Create project structure: src/, tests/, docs/
- [ ] T002 Initialize .NET 10.0 solution (slnx) in root
- [ ] T003 [P] Create API project in src/ with EntityFrameworkCore, Swashbuckle.AspNetCore
- [ ] T004 [P] Create test project in tests/ with xunit.v3, FakeItEasy
- [ ] T005 [P] Configure PostgreSQL connection in appsettings.Development.json
- [ ] T006 [P] Add Kafka configuration to appsettings.Development.json
- [ ] T007 [P] Add launchSettings.json with ASPNETCORE_ENVIRONMENT=Development
- [ ] T008 [P] Add initial README in docs/

## Phase 2: Foundational (Blocking Prerequisites)

- [ ] T009 Implement base DbContext and entity configurations in src/
- [ ] T010 [P] Implement migration logic in Program.cs for Development environment
- [ ] T011 [P] Add XML documentation to all domain entities and DTOs
- [ ] T012 [P] Implement extension methods for DTO conversions in src/

## Phase 3: User Story 1 - Criar País (P1)

- [ ] T013 [US1] Implement Pais entity and DTOs in src/Entities and src/DTOs
- [ ] T014 [US1] Implement PaisController with Create (POST /paises/post) in src/Controllers
- [ ] T015 [US1] Implement validation for Pais (Nome, CodigoIso) in src/Validators
- [ ] T016 [US1] Implement Kafka producer for Pais create in src/Kafka
- [ ] T017 [US1] Emit Kafka event on Pais creation (topic: geografia.pais, key: Id)
- [ ] T018 [US1] Add Swagger documentation for Pais endpoints
- [ ] T019 [US1] Add unit tests for PaisController Create in tests/

## Phase 4: User Story 2 - Listar Estados (P1)

- [ ] T020 [US2] Implement Estado entity and DTOs in src/Entities and src/DTOs
- [ ] T021 [US2] Implement EstadoController with List (GET /estados/get) in src/Controllers
- [ ] T022 [US2] Implement dynamic filtering for Estado in src/Controllers
- [ ] T023 [US2] Add Swagger documentation for Estado endpoints
- [ ] T024 [US2] Add unit tests for EstadoController List in tests/

## Phase 5: User Story 3 - Atualizar Cidade (P2)

- [ ] T025 [US3] Implement Cidade entity and DTOs in src/Entities and src/DTOs
- [ ] T026 [US3] Implement CidadeController with Update (PUT /cidades/put) in src/Controllers
- [ ] T027 [US3] Implement validation for Cidade update in src/Validators
- [ ] T028 [US3] Implement Kafka producer for Cidade update in src/Kafka
- [ ] T029 [US3] Emit Kafka event on Cidade update (topic: geografia.cidade, key: Id)
- [ ] T030 [US3] Add Swagger documentation for Cidade endpoints
- [ ] T031 [US3] Add unit tests for CidadeController Update in tests/

## Phase 6: User Story 4 - Remover Estado (P2)

- [ ] T032 [US4] Implement soft delete (Active=false) for Estado in src/Entities
- [ ] T033 [US4] Implement EstadoController Remove (DELETE /estados/delete) in src/Controllers
- [ ] T034 [US4] Implement Kafka producer for Estado delete in src/Kafka
- [ ] T035 [US4] Emit Kafka event on Estado delete (topic: geografia.estado, key: Id)
- [ ] T036 [US4] Add unit tests for EstadoController Remove in tests/

## Phase 7: User Story 5 - Consultar País por Id (P3)

- [ ] T037 [US5] Implement PaisController GetById (GET /paises/get/{id}) in src/Controllers
- [ ] T038 [US5] Add Swagger documentation for GetById
- [ ] T039 [US5] Add unit tests for PaisController GetById in tests/

## Final Phase: Polish & Cross-Cutting Concerns

- [ ] T040 [P] Add commented JWT authentication filter globally in src/
- [ ] T041 [P] Ensure all endpoints are kebab-case, plural, and versioned
- [ ] T042 [P] Review and update Swagger/OpenAPI documentation
- [ ] T043 [P] Add logging and correlation IDs for all API actions and Kafka events
- [ ] T044 [P] Validate performance (≤200ms p95) and compliance (ISO codes)
- [ ] T045 [P] Final code review and documentation update in docs/

## Dependencies

- Setup and Foundational phases must be completed before any user story phases
- User stories can be implemented in parallel if they do not share files
- Polish phase can start after all user stories are complete

## Parallel Execution Examples

- T003, T004, T005, T006, T007, T008 can run in parallel
- T011, T012 can run in parallel after T009
- User story phases (T013–T039) can be parallelized by entity

## Implementation Strategy

- MVP: Complete all P1 user stories (T013–T024)
- Incremental delivery: Add P2 and P3 stories, then polish

---

**All tasks follow the strict checklist format.**
