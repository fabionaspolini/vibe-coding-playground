# Documentação da Arquitetura - Onion Architecture

## Visão Geral

Este projeto segue a **Onion Architecture** (arquitetura em camadas), um padrão de design que promove a separação de responsabilidades e facilita a manutenção e testes da aplicação.

## Camadas

### 1. Domain Layer (Camada de Domínio)

**Localização**: `src/Geografia.Domain/`

**Responsabilidade**: Conter a lógica de negócio central, entidades e regras de domínio.

**Componentes**:
- **Entities**: Modelos de dados do domínio (Pais, Estado, Cidade)
- **Enums**: Tipos enumerados (TipoEstado)
- **Interfaces**: Contratos do domínio (se necessário)

**Características**:
- Não depende de nenhuma outra camada
- Não contém referências a bibliotecas externas (exceto System)
- Representa conceitos puros do negócio

**Exemplo**:
```csharp
public class Pais
{
    public required string Id { get; set; }
    public required string Nome { get; set; }
    // ... outros atributos
}
```

### 2. Application Layer (Camada de Aplicação)

**Localização**: `src/Geografia.Application/`

**Responsabilidade**: Orquestrar a lógica de negócio, converter dados e coordenar operações.

**Componentes**:
- **DTOs (Data Transfer Objects)**: Modelos para requisições/respostas da API
  - `CriarPaisDto`: Para criação
  - `AtualizarPaisDto`: Para atualização
  - `PaisDto`: Para resposta
- **Services**: Lógica de aplicação (CRUD)
  - `IPaisService` e `PaisService`
  - `IEstadoService` e `EstadoService`
  - `ICidadeService` e `CidadeService`
- **Extensions**: Métodos de extensão para conversão entre entidades e DTOs
  - `PaisExtensions`
  - `EstadoExtensions`
  - `CidadeExtensions`

**Características**:
- Depende da camada Domain
- Depende da camada Infrastructure (repositórios)
- Orquestra operações complexas
- Converte entre entidades e DTOs

**Exemplo de Service**:
```csharp
public class PaisService : IPaisService
{
    private readonly IRepository<Pais> _repository;
    private readonly IKafkaProducer _kafkaProducer;

    public async Task<PaisDto> CreateAsync(CriarPaisDto dto)
    {
        var pais = dto.ToPais();
        await _repository.AddAsync(pais);
        await _repository.SaveChangesAsync();
        _kafkaProducer.Produce("geografia.pais", pais.Id, new { action = "create", data = pais });
        return pais.ToDto();
    }
}
```

### 3. Infrastructure Layer (Camada de Infraestrutura)

**Localização**: `src/Geografia.Infrastructure/`

**Responsabilidade**: Implementar detalhes técnicos como persistência, eventos e configurações externas.

**Componentes**:
- **Data**: Contexto do Entity Framework Core
  - `GeografiaDbContext`: Configuração do banco de dados
- **Repositories**: Padrão Repository para acesso a dados
  - `IRepository<TEntity>`: Interface genérica
  - `Repository<TEntity>`: Implementação genérica
- **Kafka**: Produtor de eventos
  - `IKafkaProducer`: Interface
  - `KafkaProducer`: Implementação
- **Migrations**: Scripts de migração do banco de dados

**Características**:
- Depende da camada Domain
- Depende da camada Application (interfaces)
- Implementa padrões como Repository e Unit of Work
- Gerencia persistência e eventos externos

**Exemplo de DbContext**:
```csharp
public class GeografiaDbContext : DbContext
{
    public required DbSet<Pais> Paises { get; set; }
    public required DbSet<Estado> Estados { get; set; }
    public required DbSet<Cidade> Cidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações de mapping
    }
}
```

**Exemplo de Repository Genérico**:
```csharp
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(object id) 
        => await _dbSet.FindAsync(id);

    public async Task<List<TEntity>> ListAsync(Dictionary<string, object>? filters = null)
    {
        var query = _dbSet.AsQueryable();
        if (filters != null && filters.Count > 0)
            query = ApplyFilters(query, filters);
        return await query.ToListAsync();
    }
}
```

### 4. API Layer (Camada de API)

**Localização**: `src/Geografia.Api/`

**Responsabilidade**: Expor endpoints REST e configurar a aplicação.

**Componentes**:
- **Controllers**: Manipuladores de requisições HTTP
  - `PaisesController`: Endpoints de países
  - `EstadosController`: Endpoints de estados
  - `CidadesController`: Endpoints de cidades
- **Program.cs**: Configuração da aplicação (DI, middleware, etc.)
- **appsettings.json**: Configurações (conexão BD, Kafka, etc.)

**Características**:
- Depende de todas as outras camadas
- Define rotas HTTP (kebab-case)
- Configura injeção de dependência
- Mapeia DTOs para requisições/respostas

**Exemplo de Controller**:
```csharp
[ApiController]
[Route("paises")]
public class PaisesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaisDto>> Create([FromBody] CriarPaisDto dto)
    {
        var pais = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = pais.Id }, pais);
    }
}
```

## Fluxo de Dados

```
HTTP Request
    ↓
Controller (Api Layer)
    ↓
Service (Application Layer)
    ↓
Extension Methods (Application Layer)
    ↓
Repository (Infrastructure Layer)
    ↓
DbContext (Infrastructure Layer)
    ↓
Database
    ↓
Kafka Producer (Infrastructure Layer)
    ↓
Kafka Topic
```

## Injeção de Dependência

O `Program.cs` configura todas as dependências:

```csharp
// Registrar repositórios genéricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Registrar serviços
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();

// Registrar DbContext
builder.Services.AddDbContext<GeografiaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar Kafka
builder.Services.AddSingleton<IKafkaProducer>(new KafkaProducer(bootstrapServers));
```

## Padrões Implementados

### 1. Repository Pattern
Abstrai acesso a dados, permitindo trocar implementações sem afetar outras camadas.

### 2. Dependency Injection
Todas as dependências são injetadas, facilitando testes e manutenção.

### 3. DTO Pattern
Desacopla dados internos de como são representados na API.

### 4. Extension Methods
Simplifica conversão entre entidades e DTOs.

### 5. Soft Delete
Exclusões lógicas (marcar como inativo) em vez de exclusões físicas.

### 6. Event Sourcing
Produção de eventos Kafka para rastrear mudanças.

## Testes

**Localização**: `tests/Geografia.Tests/`

Os testes utilizam:
- **xUnit**: Framework de testes
- **FakeItEasy**: Mock de dependências

**Exemplo**:
```csharp
public class PaisServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldAddPaisAndProduceEvent()
    {
        // Arrange
        var dto = new CriarPaisDto { /* ... */ };
        var repository = A.Fake<IRepository<Pais>>();
        var kafkaProducer = A.Fake<IKafkaProducer>();
        var service = new PaisService(repository, kafkaProducer);

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        A.CallTo(() => repository.AddAsync(A<Pais>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
```

## Benefícios da Arquitetura

1. **Separação de Responsabilidades**: Cada camada tem um propósito claro
2. **Testabilidade**: Fácil mockar dependências
3. **Manutenibilidade**: Código organizado e modular
4. **Escalabilidade**: Novas funcionalidades isoladas
5. **Flexibilidade**: Trocar implementações sem afetar outras camadas
6. **Independência de Frameworks**: Domínio não depende de tecnologias específicas

## Estrutura de Pastas

```
src/
├── Geografia.Domain/
│   └── Entities/
│       ├── Pais.cs
│       ├── Estado.cs
│       └── Cidade.cs
├── Geografia.Application/
│   ├── DTOs/
│   │   ├── PaisDto.cs
│   │   ├── EstadoDto.cs
│   │   └── CidadeDto.cs
│   ├── Services/
│   │   ├── PaisService.cs
│   │   ├── EstadoService.cs
│   │   └── CidadeService.cs
│   └── Extensions/
│       └── DtoExtensions.cs
├── Geografia.Infrastructure/
│   ├── Data/
│   │   ├── GeografiaDbContext.cs
│   │   └── Migrations/
│   ├── Repositories/
│   │   └── Repository.cs
│   └── Kafka/
│       └── KafkaProducer.cs
└── Geografia.Api/
    ├── Controllers/
    │   ├── PaisesController.cs
    │   ├── EstadosController.cs
    │   └── CidadesController.cs
    ├── Program.cs
    ├── appsettings.json
    └── Properties/
        └── launchSettings.json

tests/
└── Geografia.Tests/
    └── Services/
        ├── PaisServiceTests.cs
        ├── EstadoServiceTests.cs
        └── CidadeServiceTests.cs
```

## Como Adicionar Uma Nova Entidade

1. **Domain**: Criar classe de entidade em `Entities/`
2. **Application**: Criar DTOs em `DTOs/` e services em `Services/`
3. **Infrastructure**: Adicionar DbSet no DbContext e configurar mapping
4. **API**: Criar Controller em `Controllers/`
5. **Tests**: Adicionar testes em `Tests/Services/`
6. **Migrations**: Executar `dotnet ef migrations add` e aplicar

## Conclusão

A Onion Architecture fornece uma base sólida para desenvolvimento escalável e manutenível. O código segue princípios SOLID e é fácil de testar, entender e estender.

