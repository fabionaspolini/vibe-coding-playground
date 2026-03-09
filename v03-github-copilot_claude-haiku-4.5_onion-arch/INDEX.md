# 📚 Índice de DocumAPI_EXAMPLES.http, entação - GeografiaService

## Bem-vindo ao GeografiaService!

Implementação completa de um **Sistema de Gerenciamento de Dados Geográficos** em .NET 10.0 com Onion Architecture.

---

## 📖 Documentação Disponível

### 1. **README.md** ⭐ COMECE AQUI
   - Visão geral do projeto
   - Stack tecnológico
   - Estrutura de camadas
   - Instruções de compilação e execução
   - Padrões de design implementados
   - **Tempo de leitura**: 10 min

### 2. **IMPLEMENTATION_SUMMARY.md**
   - Status completo da implementação
   - Checklist de requisitos
   - Cobertura de testes
   - Informações técnicas detalhadas
   - **Tempo de leitura**: 15 min

### 3. **STRUCTURE.md**
   - Árvore completa de diretórios
   - Descrição de cada arquivo
   - Padrões de nomenclatura
   - Relacionamentos entre projetos
   - **Tempo de leitura**: 10 min

### 4. **API_EXAMPLES.http**
   - Exemplos prontos para usar
   - Requisições POST, GET, PUT, DELETE
   - Filtros e paginação
   - **Como usar**: Abrir com REST Client do VS Code
   - **Tempo de leitura**: 5 min

### 5. **TROUBLESHOOTING.md**
   - Problemas comuns e soluções
   - Debug de erros
   - Checklist de saúde
   - Configurações avançadas
   - **Tempo de leitura**: 10 min

### 6. **quickstart.sh**
   - Script automático de inicialização
   - Configura PostgreSQL, Kafka e API
   - **Como usar**: `bash quickstart.sh`
   - **Tempo de execução**: 2 min

---

## 🚀 Quick Start (5 Minutos)

### Pré-requisitos
- Docker Desktop instalado
- .NET 10.0 SDK instalado
- Terminal/PowerShell

### Passos

```bash
# 1. Ir ao diretório do projeto
cd github-copilot_claude_haiku-4.5-onion-arch

# 2. Iniciar infraestrutura
docker-compose up -d

# 3. Aguardar 30 segundos
sleep 30

# 4. Compilar e executar testes
dotnet build
dotnet test

# 5. Executar API
cd src/GeografiaService.API
dotnet run

# 6. Acessar Swagger
# Abra: https://localhost:7200/swagger/index.html
```

---

## 📋 Arquivos do Projeto

### Raiz do Projeto
```
├── README.md                      ← Documentação principal (COMECE AQUI)
├── IMPLEMENTATION_SUMMARY.md      ← Resumo técnico da implementação
├── STRUCTURE.md                   ← Estrutura de diretórios
├── TROUBLESHOOTING.md             ← Guia de troubleshooting
├── API_EXAMPLES.http              ← Exemplos de requisições
├── quickstart.sh                  ← Script de inicialização
├── docker-compose.yml             ← Infraestrutura (PostgreSQL + Kafka)
├── GeografiaService.sln           ← Arquivo de solução
└── prompt.onion-arch.md           ← Requisitos originais
```

### Código-Fonte (`src/`)
```
src/
├── GeografiaService.Domain/       ← Entidades e Enums
├── GeografiaService.Application/  ← Services, DTOs, Extensions
├── GeografiaService.Infrastructure/ ← Repositories, EF Core, Kafka
└── GeografiaService.API/          ← Controllers, Swagger
```

### Testes (`tests/`)
```
tests/
└── GeografiaService.Tests/        ← 20 testes unitários (xUnit + FakeItEasy)
```

---

## 🎯 Guia por Cenário

### Cenário 1: "Quero entender o projeto"
1. Leia **README.md**
2. Veja **STRUCTURE.md**
3. Explore código em `src/`

**Tempo**: 30 min

### Cenário 2: "Quero executar a aplicação"
1. Execute **quickstart.sh**
2. Ou siga passos manuais em **README.md**
3. Acesse Swagger em https://localhost:7200/swagger

**Tempo**: 5 min

### Cenário 3: "Quero testar a API"
1. Abra **API_EXAMPLES.http** em VS Code
2. Use extensão REST Client
3. Execute os exemplos fornecidos

**Tempo**: 10 min

### Cenário 4: "Tenho um erro"
1. Consulte **TROUBLESHOOTING.md**
2. Siga o checklist de debug
3. Verifique logs detalhados

**Tempo**: 15 min

### Cenário 5: "Quero estudar o código"
1. Leia a documentação XML nas classes
2. Comece por **Domain** → **Application** → **Infrastructure** → **API**
3. Veja testes em `tests/` para exemplos de uso

**Tempo**: 2 horas

---

## 📊 Resumo Técnico

| Aspecto | Detalhes |
|---------|----------|
| **Framework** | .NET 10.0 |
| **Arquitetura** | Onion (4 camadas) |
| **Banco de Dados** | PostgreSQL |
| **Eventos** | Kafka |
| **API** | REST Controllers |
| **Swagger** | Habilitado e documentado |
| **Testes** | 20 testes unitários |
| **Total de Projetos** | 5 |
| **Linhas de Código** | ~3000+ |

---

## ✨ Características Principais

✅ **Onion Architecture** - Separação clara de responsabilidades  
✅ **Repository Pattern** - Abstração de dados  
✅ **Dependency Injection** - DI Container configurado  
✅ **DTOs** - Transferência de dados entre camadas  
✅ **Extension Methods** - Conversão Entity ↔ DTO  
✅ **Kafka Events** - Eventos assíncronos de CRUD  
✅ **Swagger/OpenAPI** - Documentação interativa  
✅ **Unit Tests** - 20 testes com mocks  
✅ **Migrations** - Versionamento de banco de dados  
✅ **JWT (Comentado)** - Pronto para autenticação  

---

## 🔌 Endpoints Disponíveis

### Países (`/paises`)
- `POST /paises` - Criar
- `GET /paises/{id}` - Obter por ID
- `GET /paises` - Listar com filtros
- `PUT /paises/{id}` - Atualizar
- `DELETE /paises/{id}` - Remover (soft-delete)

### Estados (`/estados`)
- `POST /estados` - Criar
- `GET /estados/{id}` - Obter por ID
- `GET /estados` - Listar com filtros (paisId, nome, ativo)
- `PUT /estados/{id}` - Atualizar
- `DELETE /estados/{id}` - Remover (soft-delete)

### Cidades (`/cidades`)
- `POST /cidades` - Criar
- `GET /cidades/{id}` - Obter por ID
- `GET /cidades` - Listar com filtros (estadoId, nome, ativo)
- `PUT /cidades/{id}` - Atualizar
- `DELETE /cidades/{id}` - Remover (soft-delete)

---

## 📱 Portas e URLs

| Serviço | Porta | URL |
|---------|-------|-----|
| API (HTTP) | 5080 | http://localhost:5080 |
| API (HTTPS) | 7200 | https://localhost:7200 |
| Swagger | 7200 | https://localhost:7200/swagger |
| PostgreSQL | 5432 | localhost:5432 |
| Kafka | 9092 | localhost:9092 |
| Zookeeper | 2181 | localhost:2181 |

---

## 🧪 Testes

Total de **20 testes unitários** cobrindo:
- ✅ PaisService (6 testes)
- ✅ EstadoService (5 testes)
- ✅ CidadeService (5 testes)
- ✅ PaisExtensions (4 testes)

Executar testes:
```bash
dotnet test
```

---

## 🐳 Docker Compose

Serviços inclusos:
- **PostgreSQL 15**: Banco de dados relacional
- **Kafka**: Message broker
- **Zookeeper**: Coordenador Kafka

Iniciar:
```bash
docker-compose up -d
```

Parar:
```bash
docker-compose down
```

Remover volumes:
```bash
docker-compose down -v
```

---

## 💡 Dicas de Produtividade

1. **Use Swagger para testar**: Acesse https://localhost:7200/swagger
2. **Use REST Client no VS Code**: Instale extensão e use API_EXAMPLES.http
3. **Monitore logs**: Use `dotnet run --verbosity diagnostic`
4. **Debug com breakpoints**: F5 no VS Code ou Visual Studio
5. **Hot reload**: Modificações de código recompilam automaticamente

---

## 📞 Suporte

Problemas? Consulte:
1. **TROUBLESHOOTING.md** para erros comuns
2. Logs detalhados com `dotnet run --verbosity diagnostic`
3. Status dos serviços com `docker ps`
4. Conectividade com `telnet localhost <porta>`

---

## 🎓 Recursos para Aprendizado

### Conceitos
- Onion Architecture
- Repository Pattern
- Dependency Injection
- Data Transfer Objects (DTOs)
- Entity Framework Core
- REST APIs
- Kafka Events

### Documentação Oficial
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Confluent Kafka .NET](https://docs.confluent.io/kafka-clients/dotnet/current/overview.html)
- [xUnit](https://xunit.net/)

---

## 📈 Próximos Passos

Após explorar o projeto:

1. **Adicionar autenticação JWT**
   - Descomente configuração em Program.cs
   - Configure servidores de autenticação

2. **Implementar paginação**
   - Adicione skip/take aos endpoints List

3. **Adicionar cache**
   - Integre Redis para dados frequentes

4. **Expandir entidades**
   - Adicione novas entidades seguindo o padrão

5. **Monitorar produção**
   - Integre Application Insights
   - Configure alertas e dashboards

---

**Última atualização: 8 de março de 2026**

**Versão: 1.0.0**

**Status: ✅ Implementação Completa**

