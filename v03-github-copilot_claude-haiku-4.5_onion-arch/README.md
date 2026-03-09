# GeografiaService

Sistema de gerenciamento de dados geográficos em .NET com Arquitetura em Camadas (Onion Architecture).

## Stack Tecnológico

- **.NET 10.0**: Framework de desenvolvimento
- **Entity Framework Core 10.0.0**: ORM para acesso a dados
- **PostgreSQL**: Banco de dados relacional
- **Kafka**: Streaming de eventos em tempo real
- **Swagger/Swashbuckle**: Documentação de APIs REST
- **xUnit**: Framework de testes unitários
- **FakeItEasy**: Mock de objetos para testes

## Estrutura do Projeto

```
GeografiaService/
├── src/
│   ├── GeografiaService.Domain/           # Camada de Domínio
│   │   └── Entities/                      # Entidades do domínio
│   ├── GeografiaService.Application/      # Camada de Aplicação
│   │   ├── DTOs/                          # Data Transfer Objects
│   │   ├── Services/                      # Serviços de negócio
│   │   └── Extensions/                    # Métodos de extensão
│   ├── GeografiaService.Infrastructure/   # Camada de Infraestrutura
│   │   ├── Data/                          # DbContext e Configurações EF Core
│   │   ├── Repositories/                  # Padrão Repository
│   │   ├── Events/                        # Integrações com Kafka
│   │   └── Migrations/                    # Migrações do banco de dados
│   └── GeografiaService.API/              # Camada de Apresentação (API)
│       └── Controllers/                   # Controllers REST
└── tests/
    └── GeografiaService.Tests/            # Testes unitários
```

## Entidades

### País
Representa um país com seus dados de identificação internacionais.

**Campos:**
- `Id` (String[2]): Código ISO 3166-1 alpha-2 (ex: "BR")
- `Nome` (String): Nome do país
- `CodigoISO3` (String[3]): Código ISO 3166-1 alpha-3
- `CodigoONU` (Integer): Código numérico da ONU
- `CodigoDDI` (String): Código de discagem internacional
- `CodigoMoeda` (String[3]): Código ISO 4217 da moeda
- `DefaultLocale` (String): Idioma padrão (ex: "pt-BR")
- `Ativo` (Boolean): Indicador de atividade (soft-delete)

### Estado
Representa uma subdivisão dentro de um país (Estado, Província, Departamento, etc).

**Campos:**
- `Id` (String[6]): Código ISO 3166-2 (ex: "BR-SP")
- `PaisId` (String[2]): Referência ao país
- `Nome` (String): Nome do estado
- `Sigla` (String): Sigla nacional
- `Tipo` (Enum): State, Province, Department, District, Region
- `Ativo` (Boolean): Indicador de atividade (soft-delete)

### Cidade
Representa uma cidade dentro de um estado.

**Campos:**
- `Id` (Guid): Identificador único (UUID v7)
- `EstadoId` (String[6]): Referência ao estado
- `Nome` (String): Nome da cidade
- `CodigoPostal` (String): Código postal (CEP/Zip)
- `Latitude` (Decimal): Coordenada de latitude
- `Longitude` (Decimal): Coordenada de longitude
- `Ativo` (Boolean): Indicador de atividade (soft-delete)

## APIs REST

### Países
- `POST /paises` - Criar novo país
- `GET /paises/{id}` - Obter país por ID
- `GET /paises` - Listar países (com filtros)
- `PUT /paises/{id}` - Atualizar país
- `DELETE /paises/{id}` - Remover país (soft-delete)

### Estados
- `POST /estados` - Criar novo estado
- `GET /estados/{id}` - Obter estado por ID
- `GET /estados` - Listar estados (com filtros)
- `PUT /estados/{id}` - Atualizar estado
- `DELETE /estados/{id}` - Remover estado (soft-delete)

### Cidades
- `POST /cidades` - Criar nova cidade
- `GET /cidades/{id}` - Obter cidade por ID
- `GET /cidades` - Listar cidades (com filtros)
- `PUT /cidades/{id}` - Atualizar cidade
- `DELETE /cidades/{id}` - Remover cidade (soft-delete)

## Eventos Kafka

Eventos são produzidos nos seguintes tópicos:
- `geografia.pais` - Eventos de País
- `geografia.estado` - Eventos de Estado
- `geografia.cidade` - Eventos de Cidade

Cada evento contém:
- **Chave**: ID da entidade
- **Valor**: Serialização JSON com dados da entidade + timestamp

## Configuração

### Dependências do Sistema
- PostgreSQL 12+ em `localhost:5432`
- Kafka em `localhost:9092`

### Credenciais Padrão
```
PostgreSQL:
  Host: localhost
  Port: 5432
  Database: Geografia
  Username: postgres
  Password: postgres

Kafka:
  Bootstrap Servers: localhost:9092
```

Altere estas configurações no arquivo `appsettings.json` se necessário.

## Compilação

```bash
# Restaurar dependências
dotnet restore

# Compilar solução
dotnet build

# Compilar em Release
dotnet build -c Release
```

## Migrations

As migrações já foram criadas. Para aplicar no banco de dados:

```bash
# Ao iniciar a aplicação em Development, as migrações são aplicadas automaticamente
```

Ou manualmente:
```bash
dotnet ef database update --project src/GeografiaService.Infrastructure --startup-project src/GeografiaService.API
```

## Executando a Aplicação

```bash
cd src/GeografiaService.API
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5080`
- HTTPS: `https://localhost:7200`

A documentação Swagger estará disponível em:
- `https://localhost:7200/swagger/index.html`

## Testes

```bash
# Executar todos os testes
dotnet test

# Executar com verbosidade
dotnet test -v d

# Executar testes específicos
dotnet test --filter "ClassName"
```

## Autenticação JWT

A autenticação JWT está implementada mas comentada em `src/GeografiaService.API/Program.cs`. Para habilitá-la:

1. Descomente as linhas de configuração JWT em `Program.cs`
2. Descomente os atributos `[Authorize]` nos Controllers
3. Configure as variáveis de autenticação em `appsettings.json`

## Padrões de Design

- **Onion Architecture**: Separação clara de responsabilidades entre camadas
- **Repository Pattern**: Abstração de acesso a dados
- **Dependency Injection**: Injeção de dependências via DI Container
- **DTO Pattern**: Uso de Data Transfer Objects para comunicação entre camadas
- **Extension Methods**: Conversão de entidades para DTOs
- **Event-Driven**: Produção de eventos no Kafka

## Convenções de Código

- **Namespaces**: File-scoped namespaces
- **Rotas**: kebab-case, sem prefixo `/api/`
- **Nomes de Recursos**: Plural (ex: `/paises/`)
- **Documentação**: XML comments para classes e métodos públicos
- **Soft-Delete**: Atributo `Ativo` para deletar logicamente

## Próximos Passos

1. Configurar PostgreSQL e Kafka localmente
2. Executar `dotnet run` para iniciar a API
3. Acessar Swagger em `https://localhost:7200/swagger`
4. Testar as APIs REST
5. Monitorar eventos no Kafka

## Licença

MIT

## Autor

Desenvolvido como exemplo de implementação de Onion Architecture com .NET 10.0

