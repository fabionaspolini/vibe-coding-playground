Setup realizado com modelo GPT 4.1.

## `/speckit.constitution` → 1. Definir constiturion

Regras imutáveis e inegociáveis do sistema.

```txt
/speckit.constitution estou criando uma api .net para ser um CRUD de dados geograficos, nesse moomento inicial deve suportar criar, atualizar, excluir e consultar Pais, Estado e Cidade.Cada ação do CRUD via API também deve gerar eventos em tópicos kafka para consumidores reagirem em tempo real.
```


## 2. `/speckit.plan` → Definir stack técnica

Definições técnicas da solução.

```txt
/speckit.plan o projeto deve respeitar a seguite stack técnica:

## Stack

- dotnet:
  - Usar target framework `net10.0`.
  - Para projeto API:
    - Todas dependências de libraries deve ser na última versão estável disponível. 
    - `Microsoft.EntityFrameworkCore`: Library principal para comunicar com banco de dados. 
    - `Swashbuckle.AspNetCore`: Library para gerar contrato swagger/open ai. Utilize **obrigatóriamente** ela e configure para usar UI.
  - Para projeto de testes:
    - `xunit.v3`: Library para testes unitários.
    - `FakeItEasy`: Library para mock de objetos.
- PostgreSQL: Como banco de dados principal.
- Kafka: Para gerar eventos de CRUD.

## Instruções gerais

- **Diretórios**:
  - Código da aplicação deve ficar na pasta `src/`.
  - Código de testes unitários deve ficar na pasta `tests/`.
- **Crie um projeto simples e direto ao ponto**:
  - Se baseie em boas práticas de desenvolvimento de software, porém não as implemente literalmente.
  - Evite interfaces e abstrações desnecessárias que as boas práticas possam lhe induzir a criar. Utilize isso apenas se for estritamente necessário.
- **API:**
  - Utilize Controllers tradicionais para rotas de API.
  - API requer autenticação por token JWT. Configure o filtro global de autenticação da aplicação, mas deixe-o comentado.
  - Rotas de exclusão devem atualizar o atributo `Active` da entidade para `false`.
  - Nomes dos métodos das controllers devem respeitar o padrão:
    - `Create` para `/post`.
    - `GetById` para `/get/{id}`.
    - `List` para `/get`.
    - `Update` para `/put`.
    - `Remove` para `/delete`.
  - **Rotas:**
    - Utilizar kebab-case (sempre lowercase).
    - Não adicionar prefixo `/api/`.
    - Resource name das rotas devem ser no plural, exemplo: `/cidades/...`.
    - O método list deve permitir filtrar por qualquer atributo.
  - Criar arquivo launchSettings.json com variável de ambiente `ASPNETCORE_ENVIRONMENT` = `Development`.
  - No startup da aplicação deve ter execução de migrations se o ambiente for `Development`.
- **Kafka:**
  - Gere eventos no Kafka para ações de create/update/delete.
  - Sempre adicionar o "Id" da entidade, como "Key" da mensagem do Kafka.
  - Nome do tópico segue o padrão `geografia.<nome-entidade>`. Exemplo: "geografia.pais"
  - Produzir mensagem com método `Produce()` invés de `ProduceAsync()`.
    - Adicionar método de callback logando eventuais falhas.
- **Design de código C#:**
  - Crie extensions methods para realizar conversões de DTOs.
  - Gere o arquivo de soluton no formato `slnx`.
  - Adicione XML documentation para todos os campos das entidades de dominio e DTO, e sempre que útil nos demais locais.
  - Não gere código de migrations, invés disso, execute a tool `dotnet ef` em meu computador para criar o arquivo de migration inicial.
  - Para métodos de apenas uma instrução, utilize expression method.
  - Utilize file scoped namespace.
- **Outras instruções:**
  - Não criar arquivo `.gitignore`.
```

## 3. `/speckit.tasks` → Definir tarefas para o plano

```txt
/speckit.tasks quebre esse plano em tarefas
```