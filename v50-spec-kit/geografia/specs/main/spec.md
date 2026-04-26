# Feature Specification: Geografia CRUD API

**Feature Branch**: `[main]`
**Created**: 2026-03-30
**Status**: Draft
**Input**: User description: "o projeto deve respeitar a seguite stack técnica: ... [detalhes da stack e instruções gerais conforme fornecido]"

## User Scenarios & Testing *(mandatory)*

**Constitutional Requirements:**
- Each user story must cover at least one CRUD operation for País, Estado, or Cidade
- For every data mutation, an event must be emitted to Kafka and testable
- API endpoints must be versioned and documented
- Security, performance, and compliance must be testable in acceptance scenarios

### User Story 1 - Criar País (Priority: P1)

Como usuário autenticado, quero criar um novo País via API para registrar dados geográficos.

**Why this priority**: É o ponto de entrada para o cadastro de toda a hierarquia geográfica.

**Independent Test**: Enviar requisição POST para /paises/post e verificar persistência e evento Kafka.

**Acceptance Scenarios**:
1. **Given** usuário autenticado, **When** envia POST válido para /paises/post, **Then** País é criado, campo Active=true, evento Kafka "geografia.pais" emitido com Id como Key.
2. **Given** usuário autenticado, **When** envia POST inválido, **Then** erro de validação retornado, nenhum evento emitido.

---

### User Story 2 - Listar Estados (Priority: P1)

Como usuário autenticado, quero listar Estados filtrando por qualquer atributo para consultar dados geográficos.

**Why this priority**: Permite navegação e consulta flexível dos dados.

**Independent Test**: Enviar GET para /estados/get com filtros e validar resposta.

**Acceptance Scenarios**:
1. **Given** usuário autenticado, **When** envia GET para /estados/get com filtros, **Then** retorna lista filtrada corretamente.
2. **Given** usuário autenticado, **When** envia GET sem filtros, **Then** retorna todos os Estados ativos.

---

### User Story 3 - Atualizar Cidade (Priority: P2)

Como usuário autenticado, quero atualizar dados de uma Cidade para manter informações corretas.

**Why this priority**: Garante integridade e atualização dos dados.

**Independent Test**: Enviar PUT para /cidades/put e validar atualização e evento Kafka.

**Acceptance Scenarios**:
1. **Given** usuário autenticado, **When** envia PUT válido para /cidades/put, **Then** Cidade é atualizada, evento Kafka "geografia.cidade" emitido.
2. **Given** usuário autenticado, **When** envia PUT inválido, **Then** erro de validação, nenhum evento emitido.

---

### User Story 4 - Remover Estado (Priority: P2)

Como usuário autenticado, quero remover um Estado para desativar dados obsoletos.

**Why this priority**: Permite controle de dados ativos sem exclusão física.

**Independent Test**: Enviar DELETE para /estados/delete, validar Active=false e evento Kafka.

**Acceptance Scenarios**:
1. **Given** usuário autenticado, **When** envia DELETE para /estados/delete, **Then** Estado tem Active=false, evento Kafka emitido.
2. **Given** usuário autenticado, **When** envia DELETE para Estado inexistente, **Then** erro 404 retornado, nenhum evento emitido.

---

### User Story 5 - Consultar País por Id (Priority: P3)

Como usuário autenticado, quero consultar um País por Id para obter detalhes específicos.

**Why this priority**: Facilita acesso rápido a informações detalhadas.

**Independent Test**: Enviar GET para /paises/get/{id} e validar resposta.

**Acceptance Scenarios**:
1. **Given** usuário autenticado, **When** envia GET para /paises/get/{id} válido, **Then** retorna detalhes do País.
2. **Given** usuário autenticado, **When** envia GET para Id inexistente, **Then** erro 404 retornado.
