# Vibe coding

Cada pasta desse projeto foi gerada por "vibe coding" utilizando varições de modelos, agentes (cli coder) e arquiteturas diferentes.

Isso é um exemplo de um projeto muito simples para CRUD de cidade, estado e país.

Funcionalidades estão melhor descritas nos arquivos de prompts de cada pasta. Eles são muito similares, mas possuem pequenas evoluções após cada geração.

Resumo de funcionalidades:

- API Rest em C#.
- PostgreSQL como persistência.
- Comandos de create, update e delete geram eventos no Kafka.
- Testes unitários.

## Padrão de nomenclatura das pasta

`(agente-cli-codificador)_(modelo-llm)_(arquitetura)`

## Notas

### v1 qwen-code_cloud-coder-model_simple-arch

**Qwen code + Cloud coder + Simple Arch**

### v2 qwen-code_cloud-coder-model_onion-arch

**Qwen code + Cloud coder + Onion arch**

### v3 - github-copilot_claude_haiku-4.5-onion-arch

**GitHub Copilot + Claude Haiku 4.5 + Onion Arch**

Não atendeu muito bem a construção de um projeto grande.

- ❌ Muitas iterações recursivas com LLM para tentar resolver problemas.
    - ❌ Criou um caracter zuado num arquivo, que precisei corrigir manualmente durante as interações.
- ✅ Gerou README.md bem detalhado.
- ✅ Criou docker-compose.yml
- ✅ Gerou documetações extras úteis: API_EXAMPLES.http, IMPLEMENTATION_SUMMARY.md, INDEX.md, STRUCTURE.md, START_HERE.txt, TROUBLESHOOTING.md
- ❌ Errou algumas importações de libraries.
- ❌ Rotas de listagem com filtros, trouxe pra memória invés de filtrar no DB.
- ❌ Gerou método async para publicar no kafka, mesmo nao precisando ser na própria implementação realizada.
- ❌ Gerou add async com entity framework.
- ❌ Inventou uma syntax desnecessária no método `GetWhereAsync`.
- ✅ Anotou bem todos os métodos com os status codes possíveis de retorno.
- ❌ Saiu com migration zuado.
- ❌ Criou interfaces de repositório no projeto de infra, errou na arquitetura.

### v4 - github-copilot-as-a-planer_claude_haiku-4.5-onion-arch

**GitHub Copilot em modo Planner + Claude Haiku 4.5 + Onion Arch**

Esse modo do agente faz ele primeiro pensar para montar um plano completo da implementação antes de começar a gerar código. Possui human in the middle para aprovar planejamento.
