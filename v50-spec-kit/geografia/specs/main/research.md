# Research: Geografia CRUD API

## Unknowns & Clarifications

- Como garantir atomicidade entre operação CRUD e emissão de evento Kafka? (NEEDS CLARIFICATION)
- Como configurar JWT auth globalmente, mas deixar comentado? (NEEDS CLARIFICATION)
- Como estruturar filtro dinâmico no método List para qualquer atributo? (NEEDS CLARIFICATION)
- Como garantir versionamento de endpoints e documentação Swagger? (NEEDS CLARIFICATION)
- Como garantir que migrations rodem apenas em ambiente Development? (NEEDS CLARIFICATION)

## Best Practices & Patterns

- Entity Framework Core para persistência e migrations
- Swashbuckle.AspNetCore para documentação OpenAPI/Swagger
- Controllers tradicionais, sem abstrações desnecessárias
- Kafka Producer síncrono (Produce), callback para logging de falhas
- Rotas kebab-case, pluralizadas, sem prefixo /api
- DTOs com extension methods para conversão
- Testes com xunit.v3 e FakeItEasy

## Decisions

- .NET 10.0 como target framework
- PostgreSQL como banco principal
- Kafka para eventos CRUD, tópico geografia.<entidade>, Id como Key
- JWT auth configurado, mas comentado
- Rotas: /paises/post, /estados/get, /cidades/put, etc.
- Soft delete via campo Active=false

## Alternatives Considered

- Outras bibliotecas de ORM (rejeitado: EF Core é padrão .NET)
- Abstrações de serviço/repositório (rejeitado: foco em simplicidade)
- Kafka Producer assíncrono (rejeitado: Produce síncrono para garantir ordem e logging)
- Filtros fixos em List (rejeitado: flexibilidade máxima)

## Rationale

- Stack enxuta, fácil de manter e expandir
- Foco em rastreabilidade, atomicidade e compliance
- Facilita onboarding e auditoria
