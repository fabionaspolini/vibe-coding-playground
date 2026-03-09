
# Guia de Contribuição

## Bem-vindo ao projeto Geografia API!

Este documento descreve como contribuir para o desenvolvimento da aplicação.

## Padrões de Código

### C# Style Guide

- **File-scoped namespaces**: Usar `namespace MeuNamespace;` em vez de chaves
- **Expression methods**: Para métodos com uma única instrução:
  ```csharp
  public int Dobra(int valor) => valor * 2;
  ```
- **XML Documentation**: Todos os public members devem ter comentários XML
  ```csharp
  /// <summary>
  /// Descrição do método.
  /// </summary>
  /// <param name="parametro">Descrição do parâmetro.</param>
  /// <returns>Descrição do retorno.</returns>
  public string MetodoExemplo(string parametro) => parametro.ToUpper();
  ```

### Convenções de Nomenclatura

- **Classes**: PascalCase (ex: `PaisService`)
- **Métodos**: PascalCase (ex: `CreateAsync`)
- **Propriedades**: PascalCase (ex: `Nome`)
- **Parâmetros**: camelCase (ex: `paisId`)
- **Variáveis locais**: camelCase (ex: `novoEstado`)
- **Constantes**: UPPER_SNAKE_CASE (ex: `MAX_RETRIES`)

### Controllers - Método HTTP Mapping

| Método HTTP | Nome do Método | Descrição             |
|-------------|----------------|-----------------------|
| POST        | Create         | Criar novo recurso    |
| GET {id}   | GetById        | Obter recurso por ID  |
| GET        | List           | Listar com filtros    |
| PUT {id}   | Update         | Atualizar recurso     |
| DELETE {id}| Remove         | Remover/desativar     |

### Nomes de Rotas

- Usar **kebab-case** (sempre lowercase)
- Usar **plural** para resource names
- Exemplos válidos:
  - `/paises` ✅
  - `/paises/{id}` ✅
  - `/estados` ✅
  - `/cidades` ✅
- Exemplos inválidos:
  - `/Paises` ❌ (não é lowercase)
  - `/pais` ❌ (não é plural)
  - `/api/paises` ❌ (não adicionar `/api/`)

## Estrutura de Commits

```
<tipo>(<escopo>): <descrição>

<corpo>

<rodapé>
```

### Tipos
- `feat`: Nova funcionalidade
- `fix`: Correção de bug
- `docs`: Mudanças de documentação
- `style`: Formatação, sem mudança de lógica
- `refactor`: Refatoração de código
- `test`: Testes
- `chore`: Dependências, configuração

### Exemplos

```
feat(paises): adicionar filtro por ativo

Permite filtrar países pelo status ativo/inativo na listagem.

Closes #123
```

```
fix(estados): corrigir validação de sigla

A sigla de estados agora é validada corretamente.
```

## Workflow de Desenvolvimento

1. **Criar feature branch**:
   ```bash
   git checkout -b feat/nova-funcionalidade
   ```

2. **Fazer mudanças** seguindo padrões de código

3. **Escrever/atualizar testes**:
   ```bash
   dotnet test
   ```

4. **Validar compilação**:
   ```bash
   dotnet build
   ```

5. **Commit e push**:
   ```bash
   git add .
   git commit -m "feat(escopo): descrição"
   git push origin feat/nova-funcionalidade
   ```

6. **Criar Pull Request**

## Adicionando Nova Entidade

### 1. Criar Entidade no Domain

**Arquivo**: `src/Geografia.Domain/Entities/MinhaEntidade.cs`

```csharp
namespace Geografia.Domain.Entities;

/// <summary>
/// Descrição da entidade.
/// </summary>
public class MinhaEntidade
{
    /// <summary>
    /// Identificador único.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Nome do atributo.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Indicador se ativo.
    /// </summary>
    public required bool Ativo { get; set; }
}
```

### 2. Criar DTOs na Application

**Arquivo**: `src/Geografia.Application/DTOs/MinhaEntidadeDto.cs`

```csharp
namespace Geografia.Application.DTOs;

public class CriarMinhaEntidadeDto
{
    public required string Id { get; set; }
    public required string Nome { get; set; }
}

public class AtualizarMinhaEntidadeDto
{
    public required string Nome { get; set; }
}

public class MinhaEntidadeDto
{
    public required string Id { get; set; }
    public required string Nome { get; set; }
    public required bool Ativo { get; set; }
}
```

### 3. Criar Extension Methods

**Arquivo**: `src/Geografia.Application/Extensions/DtoExtensions.cs` (adicionar)

```csharp
public static class MinhaEntidadeExtensions
{
    public static MinhaEntidadeDto ToDto(this MinhaEntidade entidade) => new()
    {
        Id = entidade.Id,
        Nome = entidade.Nome,
        Ativo = entidade.Ativo
    };

    public static MinhaEntidade ToMinhaEntidade(this CriarMinhaEntidadeDto dto) => new()
    {
        Id = dto.Id,
        Nome = dto.Nome,
        Ativo = true
    };

    public static void UpdateFromDto(this MinhaEntidade entidade, AtualizarMinhaEntidadeDto dto)
    {
        entidade.Nome = dto.Nome;
    }
}
```

### 4. Criar Service na Application

**Arquivo**: `src/Geografia.Application/Services/MinhaEntidadeService.cs`

```csharp
namespace Geografia.Application.Services;

public interface IMinhaEntidadeService
{
    Task<MinhaEntidadeDto> CreateAsync(CriarMinhaEntidadeDto dto);
    Task<MinhaEntidadeDto?> GetByIdAsync(string id);
    Task<List<MinhaEntidadeDto>> ListAsync(Dictionary<string, object>? filters = null);
    Task<MinhaEntidadeDto> UpdateAsync(string id, AtualizarMinhaEntidadeDto dto);
    Task RemoveAsync(string id);
}

public class MinhaEntidadeService : IMinhaEntidadeService
{
    // Implementação similar aos serviços existentes
}
```

### 5. Adicionar DbSet no Infrastructure

**Arquivo**: `src/Geografia.Infrastructure/Data/GeografiaDbContext.cs`

```csharp
public required DbSet<MinhaEntidade> MinhasEntidades { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<MinhaEntidade>(entity =>
    {
        entity.HasKey(m => m.Id);
        entity.Property(m => m.Id).HasMaxLength(2).IsRequired();
        entity.Property(m => m.Nome).IsRequired();
        entity.Property(m => m.Ativo).IsRequired();
    });
}
```

### 6. Criar Controller na API

**Arquivo**: `src/Geografia.Api/Controllers/MinhasEntidadesController.cs`

```csharp
namespace Geografia.Api.Controllers;

[ApiController]
[Route("minhas-entidades")]
public class MinhasEntidadesController : ControllerBase
{
    private readonly IMinhaEntidadeService _service;

    public MinhasEntidadesController(IMinhaEntidadeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<MinhaEntidadeDto>> Create([FromBody] CriarMinhaEntidadeDto dto)
    {
        var entidade = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = entidade.Id }, entidade);
    }

    // Implementar demais métodos (GetById, List, Update, Remove)
}
```

### 7. Registrar Serviço

**Arquivo**: `src/Geografia.Api/Program.cs`

```csharp
builder.Services.AddScoped<IMinhaEntidadeService, MinhaEntidadeService>();
```

### 8. Criar Testes

**Arquivo**: `tests/Geografia.Tests/Services/MinhaEntidadeServiceTests.cs`

```csharp
namespace Geografia.Tests.Services;

public class MinhaEntidadeServiceTests
{
    private readonly IRepository<MinhaEntidade> _repository;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly MinhaEntidadeService _service;

    public MinhaEntidadeServiceTests()
    {
        _repository = A.Fake<IRepository<MinhaEntidade>>();
        _kafkaProducer = A.Fake<IKafkaProducer>();
        _service = new MinhaEntidadeService(_repository, _kafkaProducer);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddEntidade()
    {
        // Arrange
        var dto = new CriarMinhaEntidadeDto { Id = "MN", Nome = "Teste" };
        A.CallTo(() => _repository.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        A.CallTo(() => _repository.AddAsync(A<MinhaEntidade>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
```

### 9. Criar Migration

```bash
dotnet ef migrations add Add_MinhaEntidade \
  --project src/Geografia.Infrastructure \
  --startup-project src/Geografia.Api
```

## Code Review Checklist

Antes de submeter um PR, verifique:

- [ ] Código segue convenções de nomenclatura
- [ ] XML documentation adicionada
- [ ] Testes escritos e passando
- [ ] Build compila sem erros (`dotnet build`)
- [ ] Sem warnings (se possível)
- [ ] Sem código comentado
- [ ] Sem console.WriteLine() (usar logging)
- [ ] Mensagens de commit claras
- [ ] Sem mudanças de formatação desnecessárias

## Comandos Úteis

```bash
# Compilar
dotnet build

# Testar
dotnet test

# Formatar código
dotnet format

# Limpar build
dotnet clean

# Restaurar dependências
dotnet restore

# Executar em watch mode
dotnet watch --project src/Geografia.Api
```

## Logging

Usar Microsoft.Extensions.Logging em vez de Console.WriteLine:

```csharp
public class MinhaClasse
{
    private readonly ILogger<MinhaClasse> _logger;

    public MinhaClasse(ILogger<MinhaClasse> logger)
    {
        _logger = logger;
    }

    public void MinhaFuncao()
    {
        _logger.LogInformation("Operação iniciada");
        _logger.LogWarning("Aviso importante");
        _logger.LogError("Erro encontrado");
    }
}
```

## Tratamento de Erros

Sempre validar e lançar exceções apropriadas:

```csharp
public async Task<PaisDto> GetByIdAsync(string id)
{
    if (string.IsNullOrWhiteSpace(id))
        throw new ArgumentException("ID não pode ser vazio", nameof(id));

    var pais = await _repository.GetByIdAsync(id);
    if (pais == null)
        throw new KeyNotFoundException($"País com ID {id} não encontrado");

    return pais.ToDto();
}
```

## Dúvidas?

- Consulte a documentação em `README.md`
- Verifique exemplos em código existente
- Abra uma issue para discussão

Obrigado por contribuir! 🎉

