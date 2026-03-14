#!/bin/bash

# Script de Inicialização - API de Gerenciamento de Dados Geográficos
# Este script configura e inicia a aplicação completa

set -e  # Exit on error

echo "╔════════════════════════════════════════════════════════════════╗"
echo "║  API de Gerenciamento de Dados Geográficos - Inicialização     ║"
echo "╚════════════════════════════════════════════════════════════════╝"
echo ""

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Função para imprimir mensagens coloridas
print_status() {
    echo -e "${BLUE}➜${NC} $1"
}

print_success() {
    echo -e "${GREEN}✓${NC} $1"
}

print_error() {
    echo -e "${RED}✗${NC} $1"
}

print_warning() {
    echo -e "${YELLOW}⚠${NC} $1"
}

# 1. Verificar pré-requisitos
print_status "Verificando pré-requisitos..."
echo ""

# Verificar .NET
if ! command -v dotnet &> /dev/null; then
    print_error ".NET SDK não encontrado. Instale .NET 10.0"
    exit 1
fi
print_success ".NET SDK instalado: $(dotnet --version)"

# Verificar Docker
if ! command -v docker &> /dev/null; then
    print_error "Docker não encontrado. Instale Docker"
    exit 1
fi
print_success "Docker instalado"

# Verificar Docker Compose
if ! command -v docker-compose &> /dev/null; then
    print_error "Docker Compose não encontrado. Instale Docker Compose"
    exit 1
fi
print_success "Docker Compose instalado"

echo ""

# 2. Iniciar infraestrutura
print_status "Iniciando infraestrutura (PostgreSQL, Kafka, Zookeeper)..."
echo ""

if docker-compose up -d; then
    print_success "Infraestrutura iniciada com sucesso"
else
    print_error "Erro ao iniciar infraestrutura"
    exit 1
fi

echo ""

# Esperar serviços ficarem prontos
print_status "Aguardando serviços ficarem prontos..."
sleep 5

# Verificar PostgreSQL
print_status "Verificando PostgreSQL..."
for i in {1..30}; do
    if docker exec geografia-db pg_isready -U postgres > /dev/null 2>&1; then
        print_success "PostgreSQL está pronto"
        break
    fi
    if [ $i -eq 30 ]; then
        print_error "PostgreSQL não respondeu após 30 segundos"
        exit 1
    fi
    sleep 1
done

# Verificar Kafka
print_status "Verificando Kafka..."
for i in {1..30}; do
    if docker exec geografia-kafka kafka-broker-api-versions --bootstrap-server localhost:9092 > /dev/null 2>&1; then
        print_success "Kafka está pronto"
        break
    fi
    if [ $i -eq 30 ]; then
        print_error "Kafka não respondeu após 30 segundos"
        exit 1
    fi
    sleep 1
done

echo ""

# 3. Restaurar dependências
print_status "Restaurando dependências do projeto..."
echo ""

if dotnet restore > /dev/null 2>&1; then
    print_success "Dependências restauradas"
else
    print_error "Erro ao restaurar dependências"
    exit 1
fi

echo ""

# 4. Criar migrations (se não existirem)
if [ ! -d "src/Vibe.GeografiaAPI/Migrations" ]; then
    print_status "Criando migrations iniciais..."
    echo ""

    print_warning "Você precisa executar este comando em seu computador:"
    echo ""
    echo "    cd src/Vibe.GeografiaAPI"
    echo "    dotnet ef migrations add InitialCreate"
    echo ""
    print_warning "As migrations foram estruturadas, mas precisam ser geradas localmente"
else
    print_success "Migrations já existem"
fi

echo ""

# 5. Compilar projeto
print_status "Compilando projeto..."
echo ""

if dotnet build --configuration Release > /dev/null 2>&1; then
    print_success "Projeto compilado com sucesso"
else
    print_error "Erro ao compilar projeto"
    exit 1
fi

echo ""

# 6. Exibir informações finais
echo "╔════════════════════════════════════════════════════════════════╗"
echo "║                      SETUP CONCLUÍDO!                          ║"
echo "╚════════════════════════════════════════════════════════════════╝"
echo ""

print_success "Infraestrutura pronta"
print_success "Projeto compilado"
echo ""

echo "📍 Próximas etapas:"
echo ""
echo "1. Se for primeira vez, criar migrations:"
echo "   cd src/Vibe.GeografiaAPI"
echo "   dotnet ef migrations add InitialCreate"
echo ""
echo "2. Executar a aplicação:"
echo "   cd src/Vibe.GeografiaAPI"
echo "   dotnet run"
echo ""
echo "3. Acessar a API:"
echo "   🌐 http://localhost:5177"
echo "   📚 http://localhost:5177/swagger/ui/index.html"
echo "   🔍 Kafka UI: http://localhost:8080"
echo ""
echo "4. Testar endpoints:"
echo "   📝 Abrir arquivo 'tests-api.http' com REST Client"
echo "   ou usar Swagger UI"
echo ""
echo "5. Executar testes:"
echo "   dotnet test"
echo ""

echo "📌 Serviços em execução:"
echo "   PostgreSQL: localhost:5432"
echo "   Kafka: localhost:9092"
echo "   Zookeeper: localhost:2181"
echo "   Kafka UI: http://localhost:8080"
echo ""

echo "📖 Documentação:"
echo "   📄 Leia INDEX.md para navegação"
echo "   📄 Leia FINAL_REPORT.md para visão geral"
echo "   📄 Leia SETUP.md para instruções"
echo ""

print_success "Tudo pronto! Bom desenvolvimento! 🚀"
echo ""

