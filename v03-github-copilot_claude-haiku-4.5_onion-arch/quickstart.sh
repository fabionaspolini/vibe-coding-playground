#!/bin/bash

# Script de inicialização rápida do GeografiaService
# Este script configura e executa a aplicação

set -e

PROJECT_DIR="/home/fabio/sources/samples/samples-projects/github-copilot_claude_haiku-4.5-onion-arch"

echo "🚀 GeografiaService - Quick Start"
echo "=================================="
echo ""

# Verificar se Docker está instalado
if ! command -v docker &> /dev/null; then
    echo "❌ Docker não está instalado. Por favor, instale Docker primeiro."
    exit 1
fi

# Verificar se dotnet está instalado
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK não está instalado. Por favor, instale .NET 10.0 SDK primeiro."
    exit 1
fi

echo "✅ Dependências do sistema detectadas"
echo ""

# Navegar para o diretório do projeto
cd "$PROJECT_DIR"

# Iniciar infraestrutura
echo "📦 Iniciando PostgreSQL e Kafka..."
docker-compose up -d
echo "⏳ Aguardando serviços ficarem prontos..."
sleep 15

# Compilar projeto
echo ""
echo "🔨 Compilando projeto..."
dotnet build -c Release > /dev/null 2>&1

# Executar testes
echo "🧪 Executando testes unitários..."
dotnet test --no-build --verbosity minimal

# Iniciar aplicação
echo ""
echo "🌐 Iniciando API..."
echo "📍 Swagger disponível em: https://localhost:7200/swagger/index.html"
echo ""

cd src/GeografiaService.API
dotnet run --no-build -c Release

