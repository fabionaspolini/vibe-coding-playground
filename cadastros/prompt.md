Você é um agente especialista em desenvolvimento de software. Sua tarefa é implementar uma aplicação nova.

## Stack

- dotnet:
    - EntityFramework: Library principal para comunicar com banco de dados.
    - xUnit: Library para testes unitários.
- PostgreSQL: Como banco de dados principal.
- Kafka: Para gerar eventos de CRUD.

## Instruções gerais sobre design de código

- Código da aplicação deve ficar na pasta `src/`.
- Código de testes unitários deve ficar na pasta `tests/`.
- Crie um projeto simples e direto ao ponto:
  - Se baseie em boas práticas de desenvolvimento de software, porém não implemente-as ao pé da letra.
  - Evite interfaces e abstrações desnecessárias que as boas práticas possam lhe induzir a criar. Utilize isso apenas se for estritamente necessário.
- API
  - Utilize Controllers tradicionais para rotas de API.
  - API requer autenticação por token JWT. Implemente, mas deixe comentado o código.
  - Rotas de exclusão devem atualizar o atributo `Active` da entidade para `false`. 
- Eventos de CRUD gerados no Kafka:
  - Sempre adicionar o "Id" da entidade, como "Key" da mensagem do Kafka.
  - Nome do tópico segue o padrão `geographic.<nome-entidade>`. Exemplo: "geographic.county"

## Sobre a aplicação

O objetivo dessa aplicação é gerenciar dados geográficos para um ecosistema de microserviços.

Ela deve fornecer APIs REST para CRUD (create, read, update, delete).

## Entidades

Abaixo definicação de entidades e seus atributos.
Isso compoẽ o domínio da aplicação e para cada uma deve existir a classe de entidade, model para request/response de api e migrations para controle de versionamento do banco de dados.

### Country

**Objetivo**: Gerenciar cadastro de paises.

| Atributo      | Data Type | Descrição                                                                    | Exemplo           |
|---------------|-----------|------------------------------------------------------------------------------|-------------------|
| Id            | String(2) | Udentificador único, sendo no formato do código ISO 3166-1 alpha-2 (Padrão). | "BR", "US"        |
| Name          | String    | Nome comum do país.                                                          | "Brasil"          |
| IsoCode3      | String(3) | Código ISO 3166-1 alpha-3.                                                   | "BRA"             |
| NumericCode   | Integer   | Código numérico da ONU.                                                      | 076               |
| PhoneCode     | String    | DDI (Código de discagem).                                                    | "+55"             |
| Currency      | String(3) | Código da moeda (ISO 4217).                                                  | "BRL"             |
| DefaultLocale | String    | Idioma principal.                                                            | "pt-BR", "en-US"  |
| Active        | Boolean   | Indicador se o registro ainda é válido                                       | "true" ou "false" |

### State

**Objetivo**: Gerenciar cadastro de estados.

| Atributo  | Data Type   | Descrição                                                                | Exemplo                                           |
|-----------|-------------|--------------------------------------------------------------------------|---------------------------------------------------|
| Id        | String(6)   | Identificador único, sendo no formato do código ISO 3166-2               | "BR-SP", "BR-SC", "BR-PR", "US-AK"                |
| CountryId | String(2)   | Referência a entidade `Country`.                                         |                                                   |
| Name      | String      | Nome do estado                                                           | "São Paulo", "Santa Catarina", "Paraná", "Alaska" |
| Code      | String      | Sigla nacional do estado (Código ISO 3166-2 sem a parte inicial do país) | "SP", "SC", "PR", "AK"                            |
| Category  | Enum/String | Tipo                                                                     | "STATE", "PROVINCE", "DEPARTMENT", "DISTRICT".    |
| Active    | Boolean     | Indicador se o registro ainda é válido                                   | "true" ou "false"                                 |

**Category**

Nem todos os países usam "Estado". O Canadá usa Províncias, a Colômbia usa Departamentos e a Argentina, Províncias.
Para evitar conflitos de siglas (como "SP" que poderia existir em outro país), utilize o padrão ISO 3166-2.

Exemplo: Em vez de apenas SP, o código único seria BR-SP.

### City

**Objetivo**: Gerenciar cadastro de cidades.

| Atributo   | Data Type | Descrição                              | Exemplo           |
|------------|-----------|----------------------------------------|-------------------|
| Id         | UUID      | Identificador único.                   | uuid-v7           | 
| StateId    | String(6) | Referência a `State`.                  |                   | 
| Name       | String    | Nome da cidade.                        |                   | 
| PostalCode | String    | CEP/Zip local.                         |                   | 
| Latitude   | Decimal   | Coordenada para mapas e logística.     |                   | 
| Longitude  | Decimal   | Coordenada para mapas e logística.     |                   | 
| Active     | Boolean   | Indicador se o registro ainda é válido | "true" ou "false" |

