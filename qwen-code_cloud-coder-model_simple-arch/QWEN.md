# Geografia API

## Visão Geral

Aplicação ASP.NET Core para gerenciamento de dados geográficos (países, estados e cidades) destinada a um ecossistema de microserviços. Fornece APIs REST para CRUD e publica eventos de mudanças em tópicos Kafka.

## Stack Tecnológica

- **.NET 10.0** - Framework principal
- **Entity Framework Core 9** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados relacional
- **Kafka (Confluent.Kafka)** - Mensageria para eventos de CRUD
- **Swashbuckle.AspNetCore** - Documentação Swagger/OpenAPI
- **xunit.v3 + FakeItEasy** - Testes unitários

## Estrutura do Projeto

```
geografia/
├── src/Geografia.API/          # Projeto principal da API
│   ├── Controllers/            # Controllers REST (Paises, Estados, Cidades)
│   ├── Domain/                 # Entidades de domínio (Pais, Estado, Cidade)
│   ├── DTOs/                   # Data Transfer Objects
│   ├── Extensions/             # Extension methods para conversão DTO↔Entity
│   ├── Filters/                # Filtros (ex: JwtAuthenticationAttribute)
│   ├── Infrastructure/         # DbContext e configurações de banco
│   ├── Kafka/                  # Serviços de produção de eventos Kafka
│   ├── Migrations/             # Migrations do Entity Framework
│   └── Program.cs              # Ponto de entrada da aplicação
├── tests/Geografia.Tests/      # Projeto de testes unitários
├── Geografia.slnx              # Solution file (formato XML)
└── prompt.md                   # Especificação original do projeto
```

## Entidades do Domínio

### Pais
- **Id**: String(2) - Código ISO 3166-1 alpha-2 (ex: "BR", "US")
- **Nome**: Nome comum do país
- **CodigoISO3**: String(3) - Código ISO 3166-1 alpha-3
- **CodigoONU**: Integer - Código numérico da ONU
- **CodigoDDI**: String - Código de discagem internacional
- **CodigoMoeda**: String(3) - Código ISO 4217
- **DefaultLocale**: String - Idioma principal (ex: "pt-BR")
- **Ativo**: Boolean

### Estado
- **Id**: String(6) - Código ISO 3166-2 (ex: "BR-SP")
- **PaisId**: Referência a Pais
- **Nome**: Nome do estado
- **Sigla**: Parte local do ISO 3166-2
- **Tipo**: Enum (STATE, PROVINCE, DEPARTMENT, DISTRICT)
- **Ativo**: Boolean

### Cidade
- **Id**: UUID (v7)
- **EstadoId**: Referência a Estado
- **Nome**: Nome da cidade
- **CodigoPostal**: CEP/Zip code
- **Latitude/Longitude**: Coordenadas geográficas
- **Ativo**: Boolean

## Comandos de Build e Execução

### Build
```bash
dotnet build
```

### Run (Development)
```bash
dotnet run --project src/Geografia.API/Geografia.API.csproj
```

### Testes
```bash
dotnet test
```

### Migrations
```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration -p src/Geografia.API/Geografia.API.csproj

# Atualizar banco de dados
dotnet ef database update -p src/Geografia.API/Geografia.API.csproj
```

## Configuração

### Arquivo `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=geografia;Username=postgres;Password=***"
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092"
  }
}
```

## Padrões de Desenvolvimento

### Controllers
- Rotas em **kebab-case** lowercase, sem prefixo `/api/`
- Resource names no **plural** (ex: `/paises`, `/estados`, `/cidades`)
- Métodos padronizados:
  - `Create` → `POST /`
  - `GetById` → `GET /{id}`
  - `List` → `GET /` (com filtros via query string)
  - `Update` → `PUT /{id}`
  - `Remove` → `DELETE /{id}` (soft delete: marca `Ativo = false`)

### Kafka
- Tópicos no formato: `geografia.<entidade>` (ex: `geografia.pais`)
- Key da mensagem: Id da entidade
- Payload: `KafkaEvent<T>` com Action (CREATE/UPDATE/DELETE), Data e Timestamp

### Código C#
- **File-scoped namespaces**
- **Expression-bodied members** para métodos de uma instrução
- **XML documentation** em entidades, DTOs e métodos públicos
- Extension methods para conversões DTO ↔ Entity
- JWT Authentication filter configurado globalmente (comentado por padrão)

### Convenções de Banco de Dados
- Soft delete via campo `Ativo` (não remove registros fisicamente)
- Migrations gerenciadas via EF Core CLI

## Dependências Principais

| Pacote | Versão |
|--------|--------|
| Microsoft.EntityFrameworkCore | 9.0.1 |
| Npgsql.EntityFrameworkCore.PostgreSQL | 9.0.3 |
| Swashbuckle.AspNetCore | 7.3.1 |
| Confluent.Kafka | 2.8.0 |
| xunit.v3 | 1.1.0 |
| FakeItEasy | 8.3.0 |

## Swagger UI

Acesso em desenvolvimento: `http://localhost:5000/swagger`
