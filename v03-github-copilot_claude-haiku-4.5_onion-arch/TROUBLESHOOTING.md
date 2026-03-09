# Guia de Troubleshooting - GeografiaService

## Problemas Comuns e Soluções

---

## 🔴 Problemas de Compilação

### Problema: "The type or namespace name 'X' could not be found"

**Causa**: Dependências não restauradas ou referências circulares

**Solução**:
```bash
dotnet clean
dotnet restore
dotnet build
```

### Problema: "Certificado SSL/TLS não confiável"

**Causa**: Certificado auto-assinado do localhost

**Solução (Windows PowerShell)**:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
# Na primeira execução, aceite o certificado auto-assinado no navegador
```

**Solução (Linux/Mac)**:
```bash
# Usar HTTP ao invés de HTTPS em desenvolvimento
# Ou adicionar exceção para localhost
```

---

## 🔴 Problemas de Banco de Dados

### Problema: "Unable to connect to PostgreSQL"

**Causa**: PostgreSQL não está rodando

**Solução**:
```bash
# Verificar se Docker está rodando
docker ps

# Reiniciar containers
docker-compose down
docker-compose up -d

# Aguardar 30 segundos para PostgreSQL inicializar
sleep 30
```

### Problema: "Connection timeout expired"

**Causa**: Connection string incorreta ou PostgreSQL não acessível

**Verificar**:
- Arquivo `appsettings.json`
- Porta 5432 acessível
- Credenciais PostgreSQL (postgres/postgres)

```bash
# Testar conexão
telnet localhost 5432
```

### Problema: "Migration 'InitialCreate' could not be applied"

**Causa**: Banco de dados já existe com schema diferente

**Solução**:
```bash
# Dropar e recriar banco de dados
docker-compose down -v
docker-compose up -d
sleep 30
dotnet run
```

---

## 🔴 Problemas de Kafka

### Problema: "Unable to connect to Kafka broker"

**Causa**: Kafka não está rodando ou não acessível

**Solução**:
```bash
# Verificar status do Kafka
docker ps | grep kafka

# Logs do Kafka
docker logs geografia-kafka

# Reiniciar
docker-compose down
docker-compose up -d
sleep 45  # Kafka demora mais para iniciar
```

### Problema: "Topic 'geografia.pais' does not exist"

**Causa**: Tópicos não criados automaticamente

**Solução**: 
Kafka está configurado com `auto.create.topics.enable: 'true'`. Os tópicos serão criados automaticamente na primeira mensagem. Se quiser criar manualmente:

```bash
# Entrar no container Kafka
docker exec -it geografia-kafka bash

# Criar tópicos
kafka-topics --create --topic geografia.pais --bootstrap-server localhost:9092
kafka-topics --create --topic geografia.estado --bootstrap-server localhost:9092
kafka-topics --create --topic geografia.cidade --bootstrap-server localhost:9092
```

---

## 🔴 Problemas de API/Swagger

### Problema: "404 Not Found" ao acessar Swagger

**Causa**: Swagger não foi habilitado

**Verificar**:
1. `Program.cs` tem `app.UseSwagger()` habilitado
2. URL correta: `https://localhost:7200/swagger/index.html`
3. Certificado SSL aceito

### Problema: "POST /paises returns 500 Internal Server Error"

**Causas possíveis e soluções**:

1. **Validation Error**: Verifique se todos os campos obrigatórios foram enviados
```json
// Exemplo correto:
{
  "id": "BR",
  "nome": "Brasil",
  "codigoISO3": "BRA",
  "codigoONU": 76,
  "codigoDDI": "+55",
  "codigoMoeda": "BRL",
  "defaultLocale": "pt-BR"
}
```

2. **Database Connection Error**: Verificar PostgreSQL
3. **Kafka Connection Error**: Verificar Kafka

**Debug**:
```bash
# Ver logs detalhados
dotnet run --verbosity diagnostic
```

### Problema: "CORS Error" ao chamar API do navegador

**Solução**: CORS não está configurado. Adicionar em `Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Depois em middleware:
app.UseCors();
```

---

## 🔴 Problemas de Testes

### Problema: "Tests are not discovered"

**Causa**: Projeto de testes não está referenciando corretamente

**Solução**:
```bash
# Limpar cache do teste
dotnet test --no-restore --no-build

# Recompilar
dotnet build
dotnet test
```

### Problema: "FakeItEasy setup failed"

**Causa**: Incompatibilidade de versão

**Solução**:
```bash
# Atualizar FakeItEasy
dotnet add package FakeItEasy --version 9.0.1
```

### Problema: "Test timeout"

**Solução**: Aumentar timeout em `xunit.runner.json`:
```json
{
  "longRunningTestSeconds": 30
}
```

---

## 🔴 Problemas de Performance

### Problema: "API está lenta"

**Causas possíveis**:
1. **Banco de dados**: Sem índices ou query ineficiente
   - Verificar `GeografiaDbContext.OnModelCreating()` tem índices
   
2. **Kafka lento**: Produção bloqueante
   - Considerar usar `ProduceAsync()` no futuro
   
3. **Muitos dados**: Sem paginação
   - Implementar paginação na camada Application

**Solução**:
```bash
# Profile a aplicação
dotnet trace collect -p <PID>
dotnet trace convert <trace-file>
```

---

## 🟡 Warnings Comuns (não-críticos)

### Warning: "The 'DbSet' property is not mapped to any table"

**Solução**: Garantir que o `DbSet` está em `OnModelCreating()`

### Warning: "Nullable reference type issues"

**Solução**: Verificar anotações de nullable:
```csharp
public required string Nome { get; set; }  // Obrigatório
public string? Descricao { get; set; }     // Opcional (nullable)
```

---

## ✅ Verificação de Saúde (Health Check)

### Verificar se tudo está funcionando:

```bash
# 1. Verificar Docker
docker ps

# 2. Verificar conectividade do banco
telnet localhost 5432

# 3. Verificar Kafka
docker logs geografia-kafka | tail -20

# 4. Compilar
dotnet build

# 5. Executar testes
dotnet test

# 6. Executar aplicação
cd src/GeografiaService.API
dotnet run

# 7. Acessar Swagger
# Navigate to: https://localhost:7200/swagger/index.html
```

---

## 📞 Checklist de Debug

Para cada erro, verificar nesta ordem:

1. **Logs da Aplicação**
   ```bash
   # Ver logs detalhados
   dotnet run --verbosity diagnostic
   ```

2. **Logs do Docker**
   ```bash
   docker logs geografia-postgres
   docker logs geografia-kafka
   docker logs geografia-zookeeper
   ```

3. **Verificar Conectividade**
   ```bash
   telnet localhost 5432   # PostgreSQL
   telnet localhost 9092   # Kafka
   ```

4. **Limpar e Reconstruir**
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   ```

5. **Resetar Infraestrutura**
   ```bash
   docker-compose down -v
   docker-compose up -d
   ```

---

## 🔧 Configurações Avançadas

### Mudar porta da API

**Em `Properties/launchSettings.json`**:
```json
{
  "http": {
    "applicationUrl": "http://localhost:5000",
    ...
  },
  "https": {
    "applicationUrl": "https://localhost:5001",
    ...
  }
}
```

### Mudar conexão do PostgreSQL

**Em `appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=seu-host;Port=5432;Database=Geografia;Username=user;Password=pass"
  }
}
```

### Mudar bootstrap servers do Kafka

**Em `Program.cs`**:
```csharp
var kafkaConfig = new ProducerConfig { 
    BootstrapServers = "seu-kafka:9092" 
};
```

---

## 📚 Recursos Úteis

- **Documentação .NET**: https://docs.microsoft.com/en-us/dotnet/
- **Entity Framework Core**: https://docs.microsoft.com/en-us/ef/core/
- **Confluent Kafka .NET**: https://github.com/confluentinc/confluent-kafka-dotnet
- **xUnit**: https://xunit.net/
- **FakeItEasy**: https://fakeiteasy.github.io/

---

## 💬 Suporte

Se o problema persistir:

1. Verificar arquivo de log em `bin/Debug/net10.0/`
2. Executar `dotnet new console --help` para verificar ambiente
3. Limpar completamente: `rm -rf bin obj .vs`
4. Clonar repositório novamente

---

**Última atualização: 8 de março de 2026**

