# API de Geografia - Gerenciamento de Dados Geográficos

Sistema de gerenciamento de dados geográficos (países, estados e cidades) implementado em .NET 10.0 com arquitetura em camadas (Onion Architecture).

## Tecnologias

- **.NET 10.0**
- **PostgreSQL** - Banco de dados relacional
- **Kafka** - Eventos de CRUD
- **Entity Framework Core** - ORM para acesso a dados
- **Swagger/OpenAPI** - Documentação de API
- **xUnit** - Framework de testes
- **FakeItEasy** - Mock de objetos para testes

## Estrutura do Projeto

```
├── src/
│   ├── Geografia.Domain/          # Camada de Domínio - Entidades e regras de negócio
│   ├── Geografia.Application/     # Camada de Aplicação - DTOs, Services, Extensions
│   ├── Geografia.Infrastructure/  # Camada de Infraestrutura - DbContext, Repositórios, Kafka
│   └── Geografia.Api/             # Camada de API - Controllers, Configuração
└── tests/
    └── Geografia.Tests/           # Testes Unitários
```

## Entidades

### País (Pais)
- **Id** (String[2]) - Código ISO 3166-1 alpha-2 (ex: BR, US)
- **Nome** - Nome comum do país
- **CodigoISO3** - Código ISO 3166-1 alpha-3
- **CodigoONU** - Código numérico da ONU
- **CodigoDDI** - DDI/Código de discagem
- **CodigoMoeda** - Código ISO 4217 da moeda
- **DefaultLocale** - Idioma padrão (ex: pt-BR)
- **Ativo** - Status do registro

### Estado/Província (Estado)
- **Id** (String[6]) - Código ISO 3166-2 (ex: BR-SP)
- **PaisId** - Referência ao País
- **Nome** - Nome do estado/província/departamento
- **Sigla** - Sigla (ex: SP, SC)
- **Tipo** - Enum: State, Province, Department, District
- **Ativo** - Status do registro

### Cidade (Cidade)
- **Id** (UUID) - Identificador único
- **EstadoId** - Referência ao Estado
- **Nome** - Nome da cidade
- **CodigoPostal** - CEP/Zip
- **Latitude** - Coordenada geográfica
- **Longitude** - Coordenada geográfica
- **Ativo** - Status do registro

## Configuração e Execução

### Pré-requisitos

- .NET 10.0 SDK ou superior
- PostgreSQL 12+
- Kafka (opcional, para eventos)

### Instalação

1. **Clone o repositório** (se aplicável)
```bash
cd github-copilot-as-a-planer_claude_haiku-4.5-onion-arch
```

2. **Configure o banco de dados** no arquivo `src/Geografia.Api/appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=geografia;Username=postgres;Password=postgres"
}
```

3. **Configure o Kafka** (opcional) no mesmo arquivo:
```json
"Kafka": {
    "BootstrapServers": "localhost:9092"
}
```

4. **Compile o projeto**:
```bash
dotnet build
```

5. **Execute as migrations** (em ambiente Development):
```bash
dotnet run --project src/Geografia.Api
```

As migrations serão executadas automaticamente no startup.

## APIs REST

### Endpoints - Países

- **POST** `/paises` - Criar país
- **GET** `/paises/{id}` - Obter país pelo ID
- **GET** `/paises` - Listar países (com filtros)
- **PUT** `/paises/{id}` - Atualizar país
- **DELETE** `/paises/{id}` - Remover/desativar país

### Endpoints - Estados

- **POST** `/estados` - Criar estado
- **GET** `/estados/{id}` - Obter estado pelo ID
- **GET** `/estados` - Listar estados (com filtros)
- **PUT** `/estados/{id}` - Atualizar estado
- **DELETE** `/estados/{id}` - Remover/desativar estado

### Endpoints - Cidades

- **POST** `/cidades` - Criar cidade
- **GET** `/cidades/{id}` - Obter cidade pelo ID
- **GET** `/cidades` - Listar cidades (com filtros)
- **PUT** `/cidades/{id}` - Atualizar cidade
- **DELETE** `/cidades/{id}` - Remover/desativar cidade

### Filtros

O endpoint `GET` de listagem suporta filtros dinâmicos por qualquer atributo:

```
GET /paises?Nome=Brasil&Ativo=true
GET /estados?PaisId=BR&Ativo=true
GET /cidades?EstadoId=BR-SP&Ativo=true
```

## Documentação Swagger

Após iniciar a aplicação, acesse a documentação Swagger:
```
https://localhost:7001/swagger/ui/index.html
```

## Eventos Kafka

A aplicação produz eventos no Kafka para operações de CRUD:

- Tópico: `geografia.pais` - Eventos de países
- Tópico: `geografia.estado` - Eventos de estados
- Tópico: `geografia.cidade` - Eventos de cidades

Cada evento contém:
- **key**: ID da entidade
- **value**: JSON com `action` (create/update/delete) e `data` (entidade)

## Arquitetura

### Domain Layer
- Entidades de domínio
- Tipos de valor
- Exceções de domínio

### Application Layer
- DTOs (Data Transfer Objects)
- Services/Use Cases
- Extension Methods para conversão

### Infrastructure Layer
- Entity Framework DbContext
- Repositório Genérico
- Produtor Kafka
- Migrations

### API Layer
- Controllers REST
- Configuração de Middleware
- Program.cs com DI

## Autenticação JWT

A autenticação JWT está **comentada** no código. Para ativar:

1. Descomente `app.UseAuthorization()` em `Program.cs`
2. Descomente `[Authorize]` nos Controllers
3. Configure JWT no `appsettings.json`

## Testes

Executar testes:
```bash
dotnet test
```

## Notas de Desenvolvimento

- **Soft Delete**: Exclusões utilizam soft delete (atualizam `Ativo = false`)
- **Migrations**: Arquivo de migration gerado em `src/Geografia.Infrastructure/Migrations/`
- **Async/Await**: Todos os métodos de acesso a dados são assíncronos
- **Validação**: Implementar validação de negócio conforme necessário
- **Logging**: Configurar logging conforme necessário

## Próximas Melhorias

- [ ] Implementar autenticação JWT
- [ ] Adicionar testes unitários completos
- [ ] Implementar cache
- [ ] Adicionar rate limiting
- [ ] Implementar soft delete com query filters
- [ ] Adicionar versionamento de API
- [ ] Containerizar com Docker

## Suporte

Para dúvidas ou problemas, consulte a documentação do .NET, Entity Framework Core e Kafka.

