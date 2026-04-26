# Estado API Contract

## Endpoints

- POST /estados/post
- GET /estados/get/{id}
- GET /estados/get
- PUT /estados/put
- DELETE /estados/delete

## Request/Response
- Todos endpoints usam e retornam DTOs documentados
- JWT obrigatório (comentado)
- Swagger/OpenAPI disponível

## Kafka
- Tópico: geografia.estado
- Key: Id do Estado
- Evento emitido em create/update/delete
