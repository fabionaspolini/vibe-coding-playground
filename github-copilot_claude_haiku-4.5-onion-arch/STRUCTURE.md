# Estrutura Completa do Projeto GeografiaService

## Árvore de Diretórios

```
github-copilot_claude_haiku-4.5-onion-arch/
│
├── src/                                               # Código-fonte da aplicação
│   │
│   ├── GeografiaService.Domain/                      # Camada de Domínio (entities, enums)
│   │   ├── Entities/
│   │   │   ├── Pais.cs                               # Entidade de País
│   │   │   ├── Estado.cs                             # Entidade de Estado
│   │   │   └── Cidade.cs                             # Entidade de Cidade
│   │   ├── Enums/
│   │   │   └── EstadoTipo.cs                         # Enum com tipos de subdivisões
│   │   ├── GeografiaService.Domain.csproj            # Arquivo de projeto
│   │   └── bin/, obj/                                # Diretórios de compilação
│   │
│   ├── GeografiaService.Application/                 # Camada de Aplicação (lógica de negócio)
│   │   ├── DTOs/                                     # Data Transfer Objects
│   │   │   ├── CreatePaisRequest.cs                  # DTO para criar País
│   │   │   ├── UpdatePaisRequest.cs                  # DTO para atualizar País
│   │   │   ├── PaisResponse.cs                       # DTO de resposta de País
│   │   │   ├── CreateEstadoRequest.cs                # DTO para criar Estado
│   │   │   ├── UpdateEstadoRequest.cs                # DTO para atualizar Estado
│   │   │   ├── EstadoResponse.cs                     # DTO de resposta de Estado
│   │   │   ├── CreateCidadeRequest.cs                # DTO para criar Cidade
│   │   │   ├── UpdateCidadeRequest.cs                # DTO para atualizar Cidade
│   │   │   └── CidadeResponse.cs                     # DTO de resposta de Cidade
│   │   │
│   │   ├── Services/                                 # Serviços de negócio
│   │   │   ├── PaisService.cs                        # Serviço para Países
│   │   │   ├── EstadoService.cs                      # Serviço para Estados
│   │   │   └── CidadeService.cs                      # Serviço para Cidades
│   │   │
│   │   ├── Extensions/                               # Métodos de extensão para DTOs
│   │   │   ├── PaisExtensions.cs                     # Conversão DTO ↔ Entity País
│   │   │   ├── EstadoExtensions.cs                   # Conversão DTO ↔ Entity Estado
│   │   │   └── CidadeExtensions.cs                   # Conversão DTO ↔ Entity Cidade
│   │   │
│   │   ├── GeografiaService.Application.csproj       # Arquivo de projeto
│   │   └── bin/, obj/                                # Diretórios de compilação
│   │
│   ├── GeografiaService.Infrastructure/              # Camada de Infraestrutura (dados, eventos)
│   │   ├── Data/                                     # Entity Framework Core
│   │   │   ├── GeografiaDbContext.cs                 # DbContext principal
│   │   │   └── GeografiaDbContextFactory.cs          # Factory para migrations
│   │   │
│   │   ├── Repositories/                             # Padrão Repository
│   │   │   ├── IRepository.cs                        # Interface base genérica
│   │   │   ├── Repository.cs                         # Implementação base genérica
│   │   │   ├── IPaisRepository.cs                    # Interface específica
│   │   │   ├── PaisRepository.cs                     # Implementação específica
│   │   │   ├── IEstadoRepository.cs                  # Interface específica
│   │   │   ├── EstadoRepository.cs                   # Implementação específica
│   │   │   ├── ICidadeRepository.cs                  # Interface específica
│   │   │   └── CidadeRepository.cs                   # Implementação específica
│   │   │
│   │   ├── Events/                                   # Integração Kafka
│   │   │   ├── IEventProducer.cs                     # Interface do produtor
│   │   │   └── KafkaEventProducer.cs                 # Implementação Kafka
│   │   │
│   │   ├── Migrations/                               # Migrações do banco de dados
│   │   │   ├── 20260308000000_InitialCreate.cs       # Migration inicial
│   │   │   └── GeografiaDbContextModelSnapshot.cs    # Snapshot do modelo
│   │   │
│   │   ├── GeografiaService.Infrastructure.csproj    # Arquivo de projeto
│   │   └── bin/, obj/                                # Diretórios de compilação
│   │
│   └── GeografiaService.API/                         # Camada de Apresentação (API REST)
│       ├── Controllers/                              # Controllers REST
│       │   ├── PaisController.cs                     # Endpoints de Países
│       │   ├── EstadoController.cs                   # Endpoints de Estados
│       │   └── CidadeController.cs                   # Endpoints de Cidades
│       │
│       ├── Program.cs                                # Configuração principal da aplicação
│       ├── appsettings.json                          # Configurações padrão
│       ├── appsettings.Development.json              # Configurações de Development
│       ├── GeografiaService.API.csproj               # Arquivo de projeto
│       ├── GeografiaService.API.http                 # Exemplos de requisições HTTP
│       │
│       ├── Properties/
│       │   └── launchSettings.json                   # Configurações de execução
│       │
│       └── bin/, obj/                                # Diretórios de compilação
│
├── tests/                                             # Testes da aplicação
│   └── GeografiaService.Tests/                       # Projeto de testes
│       ├── Services/                                 # Testes de serviços
│       │   ├── PaisServiceTests.cs                   # 6 testes para PaisService
│       │   ├── EstadoServiceTests.cs                 # 5 testes para EstadoService
│       │   └── CidadeServiceTests.cs                 # 5 testes para CidadeService
│       │
│       ├── Extensions/                               # Testes de extension methods
│       │   └── PaisExtensionsTests.cs                # 4 testes para PaisExtensions
│       │
│       ├── GeografiaService.Tests.csproj             # Arquivo de projeto
│       └── bin/, obj/                                # Diretórios de compilação
│
├── GeografiaService.sln                              # Arquivo de solução
├── README.md                                         # Documentação principal
├── IMPLEMENTATION_SUMMARY.md                         # Resumo da implementação
├── STRUCTURE.md                                      # Este arquivo (estrutura do projeto)
├── API_EXAMPLES.http                                 # Exemplos de requisições HTTP
├── quickstart.sh                                     # Script de inicialização rápida
├── docker-compose.yml                                # Configuração Docker Compose
└── prompt.onion-arch.md                              # Requisitos originais do projeto
```

## Arquivos de Configuração

### 1. **GeografiaService.sln**
Arquivo de solução que agrupa todos os projetos:
- GeografiaService.Domain
- GeografiaService.Application
- GeografiaService.Infrastructure
- GeografiaService.API
- GeografiaService.Tests

### 2. **docker-compose.yml**
Orquestra PostgreSQL e Kafka:
- PostgreSQL 15 em porta 5432
- Zookeeper em porta 2181
- Kafka em porta 9092

### 3. **Program.cs**
Configuração principal da aplicação:
- Registro de serviços no DI Container
- Configuração Entity Framework Core
- Configuração Kafka
- Setup Swagger
- Execução de migrations em Development
- Configuração JWT (comentada)

### 4. **.csproj files**
Cada projeto possui um arquivo .csproj com:
- Target Framework: net10.0
- ImplicitUsings: enable
- Nullable: enable
- Referências entre projetos (sem ciclos)

## Padrões de Nomenclatura

### Namespaces (file-scoped)
```csharp
namespace GeografiaService.{Layer}.{Subfolder};
```

Exemplos:
- `GeografiaService.Domain.Entities`
- `GeografiaService.Application.Services`
- `GeografiaService.Infrastructure.Repositories`
- `GeografiaService.API.Controllers`

### Arquivos
- PascalCase para nomes de classes
- Matches the class name (1 classe por arquivo, exceto migrations)
- Plurais para coleções (DTOs, Services, Repositories, Controllers)

### Rotas API
- kebab-case (lowercase)
- Recursos em plural
- Sem prefixo `/api/`

Exemplos:
- `POST /paises`
- `GET /estados/{id}`
- `PUT /cidades/{id}`

## Relacionamentos entre Projetos

```
Domain
  ↓
Infrastructure → Domain
  ↓
Application → Infrastructure, Domain
  ↓
API → Application, Domain

Tests → Application, Domain
```

### Regras de Dependência
- Domain: sem dependências externas
- Application: depende apenas de Domain
- Infrastructure: depende de Domain, Libraries externas
- API: depende de Application, Infrastructure, Domain
- Tests: depende de Application, Domain, Libraries de teste

## Padrão de Responsabilidades

### Domain
- Entidades (Pais, Estado, Cidade)
- Enums (EstadoTipo)
- Interfaces de domínio
- Sem lógica de negócio complexa

### Application
- Services com lógica de negócio
- DTOs para comunicação
- Extension methods
- Sem dependências de frameworks

### Infrastructure
- DbContext e Migrations
- Repositories (implementação)
- Event Producers (Kafka)
- Acesso a dados e eventos

### API
- Controllers REST
- Roteamento
- Serialização/Desserialização
- Configuração de middleware

### Tests
- Testes unitários de Services
- Testes de Extension Methods
- Mocks com FakeItEasy

## Resumo Estatístico

| Métrica | Quantidade |
|---------|-----------|
| Projetos | 5 |
| Entidades | 3 |
| DTOs | 9 |
| Services | 3 |
| Controllers | 3 |
| Repositories | 6 |
| Extension Classes | 3 |
| Test Classes | 4 |
| Total Test Methods | 20 |
| Linhas de Código | ~3000+ |

---

*Última atualização: 8 de março de 2026*

