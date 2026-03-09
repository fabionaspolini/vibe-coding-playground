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
