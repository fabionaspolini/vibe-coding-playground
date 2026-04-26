# Data Model: Geografia CRUD API

## Entidades

### País
- Id (Guid)
- Nome (string)
- CodigoIso (string, ISO 3166-1 alpha-2)
- Active (bool)

### Estado
- Id (Guid)
- Nome (string)
- CodigoIso (string, ISO 3166-2)
- PaisId (Guid, FK)
- Active (bool)

### Cidade
- Id (Guid)
- Nome (string)
- EstadoId (Guid, FK)
- Active (bool)

## Relacionamentos
- Estado pertence a País (N:1)
- Cidade pertence a Estado (N:1)

## Validações
- Nome obrigatório, tamanho máximo 100
- CodigoIso obrigatório, formato conforme padrão ISO
- Active default true

## Transições de Estado
- Remoção: Active = false (soft delete)
- Criação: Active = true
- Atualização: mantém Active
