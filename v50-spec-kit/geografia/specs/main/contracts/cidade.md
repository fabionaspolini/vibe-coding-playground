# Cidade API Contract

## Endpoints

- POST /cidades/post
- GET /cidades/get/{id}
- GET /cidades/get
- PUT /cidades/put
- DELETE /cidades/delete

## Request/Response
- Todos endpoints usam e retornam DTOs documentados
- JWT obrigatório (comentado)
- Swagger/OpenAPI disponível

## Kafka
- Tópico: geografia.cidade
- Key: Id da Cidade
- Evento emitido em create/update/delete
