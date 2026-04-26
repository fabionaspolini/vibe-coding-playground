# Quickstart: Geografia CRUD API

## Pré-requisitos
- .NET 10.0 SDK
- PostgreSQL
- Kafka

## Como rodar

1. Clone o repositório
2. Execute `dotnet restore` na raiz
3. Configure o banco PostgreSQL e Kafka (variáveis de ambiente)
4. Execute `dotnet build` em `src/`
5. Execute `dotnet run` em `src/` para iniciar a API
6. Acesse Swagger UI em `/swagger` para testar endpoints

## Observações
- JWT auth está configurado, mas comentado
- Migrations automáticas rodam apenas em ambiente Development
- Testes: `dotnet test` em `tests/`
- Eventos Kafka são emitidos em create/update/delete
- Para criar migrations, use: `dotnet ef migrations add InitialCreate --project src/`
