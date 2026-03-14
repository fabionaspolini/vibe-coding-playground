# API de Gerenciamento de Dados Geográficos

Uma aplicação REST desenvolvida em .NET 10 para gerenciar dados geográficos (países, estados e cidades) com integração Kafka para eventos de CRUD.

## Stack Tecnológico

- **.NET 10.0**: Framework principal
- **Entity Framework Core 10.0.5**: ORM para acesso a dados
- **PostgreSQL**: Banco de dados principal
- **Kafka**: Message broker para geração de eventos de CRUD
- **Swashbuckle.AspNetCore 10.1.5**: Geração de documentação Swagger/OpenAPI
- **Xunit.v3**: Framework de testes unitários
- **FakeItEasy**: Mock objects para testes

## Estrutura do Projeto

```
src/
├── Vibe.GeografiaAPI/
│   ├── Domain/Entities/          # Entidades de domínio
│   │   ├── Pais.cs
│   │   ├── Estado.cs
│   │   └── Cidade.cs
│   ├── Application/
│   │   ├── DTOs/                 # Data Transfer Objects
│   │   ├── Data/                 # DbContext
│   │   ├── Extensions/           # Métodos de extensão para mapeamento
│   │   └── Events/               # Produtor de eventos Kafka
│   ├── Presentation/
│   │   └── Controllers/          # Controllers REST
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Program.cs
tests/
├── Vibe.GeografiaAPI.Tests/      # Testes unitários
```

## Configuração

### Pré-requisitos

- .NET 10.0 SDK
- PostgreSQL 12+
- Kafka 3.x (opcional, para eventos)

### Variáveis de Ambiente

Criar um arquivo `.env` ou configurar as seguintes variáveis:

```bash
# PostgreSQL
DB_HOST=localhost
DB_PORT=5432
DB_NAME=geografia_dev
DB_USER=postgres
DB_PASSWORD=postgres

# Kafka
KAFKA_BOOTSTRAP_SERVERS=localhost:9092
```

### Banco de Dados

1. Criar o banco de dados:
```bash
createdb geografia_dev
```

2. Criar a migration inicial (necessário executar uma vez no seu computador):
```bash
cd src/Vibe.GeografiaAPI
dotnet ef migrations add InitialCreate
```

3. A migration será aplicada automaticamente ao iniciar a aplicação em ambiente Development.

## Entidades

### País (Pais)

Gerencia cadastro de países.

| Atributo      | Tipo     | Descrição                                   | Exemplo           |
|---------------|----------|---------------------------------------------|-------------------|
| Id            | String(2)| Código ISO 3166-1 alpha-2 (PK)             | "BR", "US"        |
| Nome          | String   | Nome comum do país                          | "Brasil"          |
| CodigoISO3    | String(3)| Código ISO 3166-1 alpha-3                   | "BRA"             |
| CodigoONU     | Integer  | Código numérico da ONU                      | 076               |
| CodigoDDI     | String   | Código de discagem internacional            | "+55"             |
| CodigoMoeda   | String(3)| Código da moeda (ISO 4217)                  | "BRL"             |
| DefaultLocale | String   | Idioma principal (locale)                   | "pt-BR", "en-US"  |
| Ativo         | Boolean  | Indicador de registro válido                | true/false        |

### Estado (Estado)

Gerencia cadastro de estados, províncias, departamentos e distritos.

| Atributo | Tipo     | Descrição                                  | Exemplo              |
|----------|----------|-------------------------------------------|----------------------|
| Id       | String(6)| Código ISO 3166-2 (PK)                   | "BR-SP", "US-AK"     |
| PaisId   | String(2)| Referência a País (FK)                    | "BR"                 |
| Nome     | String   | Nome da subdivisão                        | "São Paulo"          |
| Sigla    | String   | Sigla (parte ISO 3166-2 sem país)        | "SP", "AK"           |
| Tipo     | Enum     | Tipo: STATE, PROVINCE, DEPARTMENT, DISTRICT | "STATE"            |
| Ativo    | Boolean  | Indicador de registro válido              | true/false           |

### Cidade (Cidade)

Gerencia cadastro de cidades.

| Atributo     | Tipo      | Descrição                        | Exemplo       |
|--------------|-----------|----------------------------------|---------------|
| Id           | UUID      | Identificador único (PK, v7)     | uuid-v7       |
| EstadoId     | String(6) | Referência a Estado (FK)         | "BR-SP"       |
| Nome         | String    | Nome da cidade                   | "São Paulo"   |
| CodigoPostal | String    | CEP/Zip local                    | "01310-100"   |
| Latitude     | Decimal   | Coordenada de latitude           | -23.550520    |
| Longitude    | Decimal   | Coordenada de longitude          | -46.633308    |
| Ativo        | Boolean   | Indicador de registro válido     | true/false    |

## Endpoints da API

### Países

- `POST /paises` - Criar país
- `GET /paises/{id}` - Obter país por ID
- `GET /paises` - Listar países (com filtros opcionais: `nome`, `ativo`)
- `PUT /paises/{id}` - Atualizar país
- `DELETE /paises/{id}` - Remover país (soft delete)

### Estados

- `POST /estados` - Criar estado
- `GET /estados/{id}` - Obter estado por ID
- `GET /estados` - Listar estados (filtros: `paisId`, `nome`, `ativo`)
- `PUT /estados/{id}` - Atualizar estado
- `DELETE /estados/{id}` - Remover estado (soft delete)

### Cidades

- `POST /cidades` - Criar cidade
- `GET /cidades/{id}` - Obter cidade por ID (UUID)
- `GET /cidades` - Listar cidades (filtros: `estadoId`, `nome`, `codigoPostal`, `ativo`)
- `PUT /cidades/{id}` - Atualizar cidade
- `DELETE /cidades/{id}` - Remover cidade (soft delete)

## Eventos Kafka

Eventos são publicados nos seguintes tópicos após operações de CRUD:

- `geografia.pais` - Eventos de países
- `geografia.estado` - Eventos de estados
- `geografia.cidade` - Eventos de cidades

Cada evento contém:
- `action`: "created", "updated", "deleted"
- `entityId`: ID da entidade
- `timestamp`: Timestamp UTC do evento

### Exemplo de Evento

```json
{
  "action": "created",
  "entityId": "BR",
  "timestamp": "2024-03-14T10:30:45Z"
}
```

## Executar a Aplicação

### Desenvolvimento

```bash
cd src/Vibe.GeografiaAPI
dotnet run
```

A aplicação estará disponível em: `http://localhost:5177`

Swagger UI: `http://localhost:5177/swagger/ui/index.html`

### Build para Produção

```bash
dotnet publish -c Release -o ./release
```

## Configuração de Autenticação JWT

A autenticação JWT está comentada em `Program.cs`. Para habilitá-la:

1. Descomente as linhas de autenticação em `Program.cs`:
```csharp
app.UseAuthentication();
app.UseAuthorization();
```

2. Configure o provedor JWT com suas credenciais.

## Testes

Executar testes unitários:

```bash
cd tests/Vibe.GeografiaAPI.Tests
dotnet test
```

## Soft Delete

As operações DELETE não removem registros do banco, apenas marcam o campo `Ativo` como `false`. Para filtrar registros ativos, use o parâmetro `ativo=true` no endpoint `GET`.

## Licença

Desenvolvimento interno

## Notas de Desenvolvimento

- **File-scoped namespaces**: Todos os arquivos utilizam namespaces com escopo de arquivo
- **Extension Methods**: Conversões Entity ↔ DTO são realizadas via extension methods
- **XML Documentation**: Todos os campos públicos possuem documentação XML
- **Tratamento de Erros**: Erros são logados e retornados como respostas HTTP apropriadas

