# Instruções para Criar Migrations

## Criar a Migration Inicial

Após implementar o projeto completo, execute o seguinte comando no seu computador para criar o arquivo de migration inicial:

```bash
cd src/Vibe.GeografiaAPI
dotnet ef migrations add InitialCreate
```

Este comando irá:
1. Analisar as entidades (Pais, Estado, Cidade) e o DbContext
2. Criar um arquivo de migration em `Migrations/` com o padrão de nomenclatura `{Timestamp}_InitialCreate.cs`
3. O arquivo conterá as instruções SQL para criar as tabelas e relacionamentos

## Estrutura das Tabelas

A migration criará as seguintes tabelas:

### Tabela: public.Paises
```sql
CREATE TABLE Paises (
    Id VARCHAR(2) PRIMARY KEY,
    Nome VARCHAR NOT NULL,
    CodigoISO3 VARCHAR(3) NOT NULL,
    CodigoONU INT NOT NULL,
    CodigoDDI VARCHAR NOT NULL,
    CodigoMoeda VARCHAR(3) NOT NULL,
    DefaultLocale VARCHAR NOT NULL,
    Ativo BOOLEAN NOT NULL DEFAULT true
);
```

### Tabela: public.Estados
```sql
CREATE TABLE Estados (
    Id VARCHAR(6) PRIMARY KEY,
    PaisId VARCHAR(2) NOT NULL,
    Nome VARCHAR NOT NULL,
    Sigla VARCHAR NOT NULL,
    Tipo INT NOT NULL,
    Ativo BOOLEAN NOT NULL DEFAULT true,
    CONSTRAINT FK_Estados_Paises FOREIGN KEY (PaisId) REFERENCES Paises(Id) ON DELETE RESTRICT
);
```

### Tabela: public.Cidades
```sql
CREATE TABLE Cidades (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    EstadoId VARCHAR(6) NOT NULL,
    Nome VARCHAR NOT NULL,
    CodigoPostal VARCHAR NOT NULL,
    Latitude NUMERIC(9,6) NOT NULL,
    Longitude NUMERIC(9,6) NOT NULL,
    Ativo BOOLEAN NOT NULL DEFAULT true,
    CONSTRAINT FK_Cidades_Estados FOREIGN KEY (EstadoId) REFERENCES Estados(Id) ON DELETE RESTRICT
);
```

## Adicionar Novas Migrations no Futuro

Para adicionar mudanças no modelo de dados:

1. Altere as entidades (Domain/Entities/*.cs)
2. Crie uma nova migration:
```bash
dotnet ef migrations add NomeDaMigracao
```

3. A migration será aplicada automaticamente ao iniciar a aplicação em ambiente Development.

## Remover Última Migration (se necessário)

```bash
dotnet ef migrations remove
```

## Visualizar SQL Gerado

Para visualizar o SQL que será executado sem aplicar:

```bash
dotnet ef migrations script
```

## Rollback (Reverter Migrations)

Para reverter para um estado anterior (útil em desenvolvimento):

```bash
# Reverter para uma migration específica
dotnet ef database update NomeDaMigracao

# Reverter tudo
dotnet ef database update 0
```

## Notas Importantes

- As migrations são executadas automaticamente no startup quando o ambiente é `Development`
- Em produção, use `dotnet ef database update` manualmente para aplicar migrations de forma controlada
- Sempre teste as migrations em um ambiente de desenvolvimento primeiro
- Mantenha as migrations no controle de versão (git)

