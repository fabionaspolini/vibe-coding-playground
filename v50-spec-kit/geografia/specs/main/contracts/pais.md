# País API Contract

## Endpoints

- POST /paises/post
- GET /paises/get/{id}
- GET /paises/get
- PUT /paises/put
- DELETE /paises/delete

## Request/Response
- Todos endpoints usam e retornam DTOs documentados
- JWT obrigatório (comentado)
- Swagger/OpenAPI disponível

## Kafka
- Tópico: geografia.pais
- Key: Id do País
- Evento emitido em create/update/delete
