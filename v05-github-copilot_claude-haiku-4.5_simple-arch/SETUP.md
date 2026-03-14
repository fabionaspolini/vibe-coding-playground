# Instruções de Setup

## Pré-requisitos

- .NET 10.0 SDK instalado
- Docker e Docker Compose instalados
- Git instalado

## Configuração do Ambiente

### 1. Clonar o Repositório

```bash
git clone <repository-url>
cd vibe-coding-playground/v05-github-copilot_claude-haiku-4.5_simple-arch
```

### 2. Iniciar Serviços com Docker Compose

```bash
docker-compose up -d
```

Isso iniciará:
- **PostgreSQL** em `localhost:5432`
- **Kafka** em `localhost:9092`
- **Zookeeper** em `localhost:2181`
- **Kafka UI** em `http://localhost:8080` (visualizador de eventos Kafka)

Verificar se os serviços estão saudáveis:

```bash
docker-compose ps
```

### 3. Criar Migrations

A primeira vez que o projeto é compilado, você precisa criar a migration inicial:

```bash
cd src/Vibe.GeografiaAPI
dotnet ef migrations add InitialCreate
```

Isso cria um arquivo em `Migrations/` com o padrão `{Timestamp}_InitialCreate.cs`.

### 4. Restaurar Dependências

```bash
cd /path/to/project/root
dotnet restore
```

### 5. Compilar Projeto

```bash
dotnet build
```

### 6. Executar Aplicação

```bash
cd src/Vibe.GeografiaAPI
dotnet run
```

A aplicação estará disponível em:
- **API**: `http://localhost:5177`
- **Swagger UI**: `http://localhost:5177/swagger/ui/index.html`

## Testes

### Executar Testes Unitários

```bash
dotnet test
```

### Testar Endpoints com HTTP Client

O Visual Studio Code suporta requisições HTTP diretamente. Instale a extensão:
- REST Client (Huachao Mao)

Após instalar, abra o arquivo `tests-api.http` e clique em "Send Request" acima de cada bloco de teste.

Alternativamente, use `curl`:

```bash
# Criar país
curl -X POST http://localhost:5177/paises \
  -H "Content-Type: application/json" \
  -d '{
    "id": "BR",
    "nome": "Brasil",
    "codigoISO3": "BRA",
    "codigoONU": 76,
    "codigoDDI": "+55",
    "codigoMoeda": "BRL",
    "defaultLocale": "pt-BR"
  }'

# Listar países
curl http://localhost:5177/paises
```

## Monitoramento de Eventos Kafka

Acesse o Kafka UI em `http://localhost:8080` para:
- Visualizar tópicos criados
- Ver mensagens dos eventos CRUD em tempo real
- Monitorar partições e offsets

## Banco de Dados

### Conectar ao PostgreSQL

```bash
psql -h localhost -U postgres -d geografia_dev
```

Senha: `postgres`

### Comandos Úteis

```sql
-- Listar tabelas
\dt

-- Visualizar esquema de uma tabela
\d Paises

-- Contar registros
SELECT COUNT(*) FROM Paises;
```

## Troubleshooting

### Erro: "connection refused"

Verifique se os containers do Docker estão rodando:
```bash
docker-compose ps
```

Se algum estiver parado:
```bash
docker-compose up -d <nome-do-servico>
```

### Erro: "database does not exist"

A aplicação cria e aplica migrations automaticamente em ambiente Development. Se o banco não foi criado:

```bash
cd src/Vibe.GeografiaAPI
dotnet ef database update
```

### Erro: EF Core Tools não encontrado

```bash
dotnet tool install --global dotnet-ef
```

### Erro ao produzir eventos Kafka

Verifique se o Kafka está rodando:
```bash
docker-compose logs kafka
```

Se houver erro, reinicie:
```bash
docker-compose restart kafka
```

## Limpeza

Para parar todos os serviços:

```bash
docker-compose down
```

Para remover volumes de dados (para resetar o banco):

```bash
docker-compose down -v
```

## Variáveis de Ambiente

Se precisar customizar as configurações, edite os valores em:
- `src/Vibe.GeografiaAPI/appsettings.json` (padrão)
- `src/Vibe.GeografiaAPI/appsettings.Development.json` (desenvolvimento)

Ou defina variáveis de ambiente antes de executar:

```bash
export ASPNETCORE_ENVIRONMENT=Development
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=geografia_dev;Username=postgres;Password=postgres"
export Kafka__BootstrapServers="localhost:9092"
```

## Próximos Passos

1. [x] Verificar saúde da aplicação via Swagger
2. [x] Testar CRUD via `tests-api.http`
3. [x] Visualizar eventos em `localhost:8080` (Kafka UI)
4. [ ] Implementar autenticação JWT (descomentar em `Program.cs`)
5. [ ] Adicionar validações mais robustas
6. [ ] Implementar testes de integração
7. [ ] Deploy em container

## Documentação Adicional

- [README.md](./README.md) - Descrição geral da aplicação
- [MIGRATIONS.md](./MIGRATIONS.md) - Instruções sobre Entity Framework Migrations
- [prompt.simple-arch.md](./prompt.simple-arch.md) - Requisitos originais do projeto

