# Guia de Setup e Configuração

## Pré-requisitos

- **.NET 10.0 SDK**: [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **PostgreSQL 12+**: [Download](https://www.postgresql.org/download/)
- **Kafka** (opcional): [Download](https://kafka.apache.org/downloads)
- **Git**: Controle de versão

## Instalação Local

### 1. Clonar o Repositório

```bash
git clone <url-do-repositorio>
cd github-copilot-as-a-planer_claude_haiku-4.5-onion-arch
```

### 2. Restaurar Dependências

```bash
dotnet restore
```

### 3. Configurar Banco de Dados PostgreSQL

#### 3.1 Criar Banco de Dados

```bash
# No PostgreSQL CLI (psql)
psql -U postgres

# No terminal psql
CREATE DATABASE geografia;
CREATE USER geografia_user WITH PASSWORD 'sua_senha_aqui';
ALTER ROLE geografia_user SET client_encoding TO 'utf8';
ALTER ROLE geografia_user SET default_transaction_isolation TO 'read committed';
ALTER ROLE geografia_user SET default_transaction_deferrable TO on;
ALTER ROLE geografia_user SET default_time_zone TO 'UTC';
GRANT ALL PRIVILEGES ON DATABASE geografia TO geografia_user;
\q
```

#### 3.2 Atualizar Connection String

Editar `src/Geografia.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=geografia;Username=geografia_user;Password=sua_senha_aqui"
  }
}
```

### 4. Configurar Kafka (Opcional)

Se deseja usar Kafka, configure a connection string em `appsettings.json`:

```json
{
  "Kafka": {
    "BootstrapServers": "localhost:9092"
  }
}
```

Se não possuir Kafka instalado, a aplicação funcionará mesmo assim, mas os eventos não serão publicados.

#### 4.1 Iniciar Kafka Localmente (com Docker)

```bash
# Iniciar Zookeeper
docker run -d --name zookeeper \
  -e ZOOKEEPER_CLIENT_PORT=2181 \
  -e ZOOKEEPER_TICK_TIME=2000 \
  confluentinc/cp-zookeeper:7.0.1

# Iniciar Kafka
docker run -d --name kafka \
  -e KAFKA_BROKER_ID=1 \
  -e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 \
  -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://kafka:9092 \
  -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 \
  --link zookeeper \
  confluentinc/cp-kafka:7.0.1
```

### 5. Compilar o Projeto

```bash
dotnet build
```

### 6. Executar Migrations

As migrations são executadas automaticamente na inicialização em modo Development:

```bash
dotnet run --project src/Geografia.Api
```

A aplicação irá:
1. Conectar ao banco de dados
2. Executar migrations pendentes
3. Iniciar na porta `http://localhost:5001`

### 7. Acessar Swagger

Abrir no navegador:
```
https://localhost:7001/swagger/ui/index.html
```

ou

```
http://localhost:5001/swagger/ui/index.html
```

## Variáveis de Ambiente

### Development

```bash
export ASPNETCORE_ENVIRONMENT=Development
export ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=geografia;Username=postgres;Password=postgres"
export Kafka__BootstrapServers="localhost:9092"
```

### Production

```bash
export ASPNETCORE_ENVIRONMENT=Production
export ConnectionStrings__DefaultConnection="Host=seu-host;Port=5432;Database=geografia;Username=user;Password=senha"
export Kafka__BootstrapServers="seu-kafka-host:9092"
```

## Desenvolvimento Local

### Iniciar em Modo Watch

```bash
dotnet watch --project src/Geografia.Api
```

A aplicação será reiniciada automaticamente ao salvar arquivos.

### Executar Testes

```bash
dotnet test
```

Executar com cobertura de código:

```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

### Compilar em Release

```bash
dotnet build -c Release
```

## Docker

### Dockerfile (exemplo)

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5001

EXPOSE 5001
ENTRYPOINT ["dotnet", "Geografia.Api.dll"]
```

### Construir e Executar Container

```bash
# Construir imagem
docker build -t geografia-api:latest .

# Executar container
docker run -d \
  -p 5001:5001 \
  -e ConnectionStrings__DefaultConnection="Host=postgres;Port=5432;Database=geografia;Username=postgres;Password=postgres" \
  -e Kafka__BootstrapServers="kafka:9092" \
  --link postgres \
  --link kafka \
  --name geografia-api \
  geografia-api:latest
```

### Docker Compose (exemplo)

```yaml
version: '3.8'

services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: geografia
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  kafka:
    image: confluentinc/cp-kafka:7.0.1
    depends_on:
      - zookeeper
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://kafka:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
    ports:
      - "9092:9092"

  zookeeper:
    image: confluentinc/cp-zookeeper:7.0.1
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181

  api:
    build: .
    depends_on:
      - postgres
      - kafka
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: "Host=postgres;Port=5432;Database=geografia;Username=postgres;Password=postgres"
      Kafka__BootstrapServers: "kafka:9092"
    ports:
      - "5001:5001"
    command: dotnet Geografia.Api.dll

volumes:
  postgres_data:
```

Executar com Docker Compose:

```bash
docker-compose up -d
```

## Scripts Úteis

### Limpar Cache de Build

```bash
dotnet clean
rm -rf ./bin ./obj ./src/*/bin ./src/*/obj ./tests/*/bin ./tests/*/obj
```

### Restaurar e Compilar

```bash
dotnet clean && dotnet restore && dotnet build
```

### Executar Testes com Output Detalhado

```bash
dotnet test --verbosity normal
```

### Criar Nova Migration

```bash
dotnet ef migrations add NomeDaMigration \
  --project src/Geografia.Infrastructure \
  --startup-project src/Geografia.Api
```

### Reverter Última Migration

```bash
dotnet ef migrations remove \
  --project src/Geografia.Infrastructure \
  --startup-project src/Geografia.Api
```

### Visualizar SQL Gerado

```bash
dotnet ef dbcontext scaffold \
  "Host=localhost;Port=5432;Database=geografia;Username=postgres;Password=postgres" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --force
```

## Troubleshooting

### Erro: "Cannot open database connection"

**Solução**:
- Verificar se PostgreSQL está rodando
- Verificar connection string em `appsettings.json`
- Verificar credenciais do banco de dados

### Erro: "Migrations assembly not found"

**Solução**:
```bash
dotnet ef database update --project src/Geografia.Infrastructure
```

### Erro: "Kafka broker not found"

**Solução**:
- Kafka é opcional; a aplicação funcionará sem ele
- Se deseja usar, configure a `BootstrapServers` corretamente

### Porta 5001 já em uso

**Solução**:
Modificar em `launchSettings.json`:
```json
"applicationUrl": "http://localhost:5002"
```

## Checklist de Setup

- [ ] .NET 10.0 SDK instalado
- [ ] PostgreSQL instalado e rodando
- [ ] Banco de dados `geografia` criado
- [ ] Connection string configurada em `appsettings.json`
- [ ] `dotnet restore` executado
- [ ] `dotnet build` compilou sem erros
- [ ] `dotnet run` iniciou a aplicação
- [ ] Swagger acessível em `http://localhost:5001/swagger`
- [ ] Testes passando com `dotnet test`

## Próximos Passos

1. Criar um país via Swagger ou cURL
2. Criar um estado vinculado ao país
3. Criar uma cidade vinculada ao estado
4. Verificar eventos no Kafka (se configurado)
5. Executar testes
6. Explorar a documentação Swagger

## Suporte

Para problemas:
1. Consulte os logs da aplicação
2. Verifique as configurações em `appsettings.json`
3. Execute `dotnet clean` e compile novamente
4. Verifique a documentação do .NET 10.0

