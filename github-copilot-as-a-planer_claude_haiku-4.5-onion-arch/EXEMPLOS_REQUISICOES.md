# Exemplos de Requisições HTTP

Este documento contém exemplos de como utilizar a API de Geografia.

## Exemplos com cURL

### Países

#### Criar um país
```bash
curl -X POST http://localhost:5001/paises \
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
```

#### Obter um país
```bash
curl -X GET http://localhost:5001/paises/BR
```

#### Listar todos os países
```bash
curl -X GET http://localhost:5001/paises
```

#### Listar países filtrados
```bash
curl -X GET "http://localhost:5001/paises?nome=Brasil&ativo=true"
```

#### Atualizar um país
```bash
curl -X PUT http://localhost:5001/paises/BR \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Brasil",
    "codigoISO3": "BRA",
    "codigoONU": 76,
    "codigoDDI": "+55",
    "codigoMoeda": "BRL",
    "defaultLocale": "pt-BR"
  }'
```

#### Remover/desativar um país
```bash
curl -X DELETE http://localhost:5001/paises/BR
```

### Estados

#### Criar um estado
```bash
curl -X POST http://localhost:5001/estados \
  -H "Content-Type: application/json" \
  -d '{
    "id": "BR-SP",
    "paisId": "BR",
    "nome": "São Paulo",
    "sigla": "SP",
    "tipo": "State"
  }'
```

#### Obter um estado
```bash
curl -X GET http://localhost:5001/estados/BR-SP
```

#### Listar estados filtrados por país
```bash
curl -X GET "http://localhost:5001/estados?paisId=BR&ativo=true"
```

#### Atualizar um estado
```bash
curl -X PUT http://localhost:5001/estados/BR-SP \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "São Paulo",
    "sigla": "SP",
    "tipo": "State"
  }'
```

#### Remover/desativar um estado
```bash
curl -X DELETE http://localhost:5001/estados/BR-SP
```

### Cidades

#### Criar uma cidade
```bash
curl -X POST http://localhost:5001/cidades \
  -H "Content-Type: application/json" \
  -d '{
    "estadoId": "BR-SP",
    "nome": "São Paulo",
    "codigoPostal": "01310100",
    "latitude": -23.5505,
    "longitude": -46.6333
  }'
```

#### Obter uma cidade
```bash
curl -X GET http://localhost:5001/cidades/550e8400-e29b-41d4-a716-446655440000
```

#### Listar cidades filtradas por estado
```bash
curl -X GET "http://localhost:5001/cidades?estadoId=BR-SP&ativo=true"
```

#### Atualizar uma cidade
```bash
curl -X PUT http://localhost:5001/cidades/550e8400-e29b-41d4-a716-446655440000 \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "São Paulo",
    "codigoPostal": "01310100",
    "latitude": -23.5505,
    "longitude": -46.6333
  }'
```

#### Remover/desativar uma cidade
```bash
curl -X DELETE http://localhost:5001/cidades/550e8400-e29b-41d4-a716-446655440000
```

## Exemplos com Postman

### Coleção (JSON)

```json
{
  "info": {
    "name": "Geografia API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Países",
      "item": [
        {
          "name": "Criar País",
          "request": {
            "method": "POST",
            "url": {
              "raw": "{{base_url}}/paises",
              "host": ["{{base_url}}"],
              "path": ["paises"]
            },
            "body": {
              "mode": "raw",
              "raw": "{\"id\": \"BR\", \"nome\": \"Brasil\", \"codigoISO3\": \"BRA\", \"codigoONU\": 76, \"codigoDDI\": \"+55\", \"codigoMoeda\": \"BRL\", \"defaultLocale\": \"pt-BR\"}"
            }
          }
        }
      ]
    }
  ]
}
```

## Exemplos com C# HttpClient

```csharp
using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5001") };

// Criar país
var paisDto = new
{
    id = "BR",
    nome = "Brasil",
    codigoISO3 = "BRA",
    codigoONU = 76,
    codigoDDI = "+55",
    codigoMoeda = "BRL",
    defaultLocale = "pt-BR"
};

var json = JsonSerializer.Serialize(paisDto);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync("/paises", content);
var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);

// Obter país
response = await client.GetAsync("/paises/BR");
result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);

// Listar países
response = await client.GetAsync("/paises?nome=Brasil");
result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);

// Atualizar país
var updateDto = new
{
    nome = "Brasil",
    codigoISO3 = "BRA",
    codigoONU = 76,
    codigoDDI = "+55",
    codigoMoeda = "BRL",
    defaultLocale = "pt-BR"
};

json = JsonSerializer.Serialize(updateDto);
content = new StringContent(json, Encoding.UTF8, "application/json");
response = await client.PutAsync("/paises/BR", content);
result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);

// Deletar país
response = await client.DeleteAsync("/paises/BR");
Console.WriteLine(response.StatusCode); // 204 No Content
```

## Exemplo com JavaScript/Fetch API

```javascript
const baseUrl = 'http://localhost:5001';

// Criar país
async function criarPais() {
  const pais = {
    id: 'BR',
    nome: 'Brasil',
    codigoISO3: 'BRA',
    codigoONU: 76,
    codigoDDI: '+55',
    codigoMoeda: 'BRL',
    defaultLocale: 'pt-BR'
  };

  const response = await fetch(`${baseUrl}/paises`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(pais)
  });

  const data = await response.json();
  console.log(data);
}

// Obter país
async function obterPais(id) {
  const response = await fetch(`${baseUrl}/paises/${id}`);
  const data = await response.json();
  console.log(data);
}

// Listar países
async function listarPaises(filtros = {}) {
  const params = new URLSearchParams(filtros);
  const response = await fetch(`${baseUrl}/paises?${params}`);
  const data = await response.json();
  console.log(data);
}

// Atualizar país
async function atualizarPais(id, dados) {
  const response = await fetch(`${baseUrl}/paises/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(dados)
  });

  const data = await response.json();
  console.log(data);
}

// Deletar país
async function deletarPais(id) {
  const response = await fetch(`${baseUrl}/paises/${id}`, {
    method: 'DELETE'
  });

  console.log(response.status); // 204
}
```

## Códigos de Resposta HTTP

- **200 OK**: Operação bem-sucedida
- **201 Created**: Recurso criado com sucesso
- **204 No Content**: Operação bem-sucedida sem retorno de conteúdo
- **400 Bad Request**: Dados inválidos
- **404 Not Found**: Recurso não encontrado
- **500 Internal Server Error**: Erro no servidor

## Tipos de Enum - Tipo de Estado

```
State       = 0
Province    = 1
Department  = 2
District    = 3
```

Ou use strings (case-insensitive):
- "state"
- "province"
- "department"
- "district"

## Nota sobre Filtros

O método `List` suporta filtros dinâmicos por qualquer atributo da entidade:

```bash
# Filtrar por um atributo
curl -X GET "http://localhost:5001/paises?nome=Brasil"

# Filtrar por múltiplos atributos
curl -X GET "http://localhost:5001/paises?nome=Brasil&ativo=true"

# Filtrar Estados por País
curl -X GET "http://localhost:5001/estados?paisId=BR&ativo=true"

# Filtrar Cidades por Estado
curl -X GET "http://localhost:5001/cidades?estadoId=BR-SP&nome=São Paulo"
```

**Note**: Os nomes dos filtros devem corresponder aos nomes das propriedades das entidades (case-insensitive).

