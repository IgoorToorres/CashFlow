# CashFlow API

<p align="center">
  <strong>API REST para controle de despesas pessoais, com cadastro, consulta, edição, remoção e geração de relatórios financeiros.</strong>
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img alt="ASP.NET Core" src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img alt="PostgreSQL" src="https://img.shields.io/badge/PostgreSQL-16-4169E1?style=for-the-badge&logo=postgresql&logoColor=white">
  <img alt="Docker" src="https://img.shields.io/badge/Docker-Compose-2496ED?style=for-the-badge&logo=docker&logoColor=white">
</p>

---

## Sumário

- [Sobre o projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Bibliotecas](#bibliotecas)
- [Como rodar](#como-rodar)
- [Endpoints](#endpoints)
- [Testes](#testes)
- [Estrutura de pastas](#estrutura-de-pastas)

## Sobre o projeto

O **CashFlow** é uma API desenvolvida em **.NET** para gerenciamento de despesas. O projeto permite registrar gastos, listar despesas cadastradas, buscar uma despesa por identificador, atualizar informações, remover registros e gerar relatórios mensais em **Excel** e **PDF**.

A aplicação foi organizada em camadas para separar responsabilidades de domínio, comunicação, regras de aplicação, infraestrutura, API e testes.

## Funcionalidades

- Cadastro de despesas.
- Listagem de despesas.
- Busca de despesa por ID.
- Atualização de despesa.
- Remoção de despesa.
- Relatório mensal em Excel.
- Relatório mensal em PDF.
- Validação de entrada com FluentValidation.
- Persistência em PostgreSQL com Entity Framework Core.
- Documentação interativa da API com Swagger.
- Tratamento centralizado de exceções.
- Suporte a mensagens localizadas em `pt-BR`.

## Arquitetura

O projeto segue uma separação em camadas inspirada em Clean Architecture:

| Camada | Responsabilidade |
| --- | --- |
| `CashFlow.API` | Entrada da aplicação, controllers, filtros, middlewares e Swagger. |
| `CashFlow.Application` | Casos de uso, validações, mapeamentos e geração de relatórios. |
| `CashFlow.Domain` | Entidades, enums, contratos de repositório e recursos de domínio. |
| `CashFlow.Infrastructure` | Acesso a dados, DbContext, repositórios, Unit of Work e migrations. |
| `CashFlow.Communication` | DTOs de request/response e contratos de comunicação externa. |
| `CashFlow.Exception` | Exceções customizadas e mensagens de erro. |
| `CashFlow.Tests` | Testes automatizados da aplicação. |
| `CashFlow.CommonTestUtilities` | Builders e utilitários compartilhados pelos testes. |

## Tecnologias

| Tecnologia | Uso no projeto |
| --- | --- |
| .NET 10 | Plataforma principal da aplicação. |
| ASP.NET Core Web API | Criação dos endpoints HTTP. |
| Entity Framework Core | ORM para persistência de dados. |
| PostgreSQL 16 | Banco de dados relacional. |
| Docker Compose | Subida do banco local de desenvolvimento. |
| Swagger/OpenAPI | Documentação e teste dos endpoints. |
| xUnit | Execução dos testes automatizados. |

## Bibliotecas

### API

| Biblioteca | Versão | Finalidade |
| --- | --- | --- |
| `Swashbuckle.AspNetCore` | `10.2.2` | Geração da interface Swagger. |
| `Microsoft.AspNetCore.OpenApi` | `10.0.8` | Suporte à especificação OpenAPI. |
| `Microsoft.EntityFrameworkCore.Design` | `10.0.9` | Suporte a tooling e migrations do EF Core. |

### Application

| Biblioteca | Versão | Finalidade |
| --- | --- | --- |
| `AutoMapper` | `12.0.1` | Mapeamento entre entidades e DTOs. |
| `AutoMapper.Extensions.Microsoft.DependencyInjection` | `12.0.1` | Integração do AutoMapper com injeção de dependência. |
| `FluentValidation` | `12.1.1` | Validação das requisições. |
| `ClosedXML` | `0.105.0` | Geração de relatórios em Excel. |
| `PDFsharp-MigraDoc` | `6.2.4` | Geração de relatórios em PDF. |
| `Microsoft.Extensions.DependencyInjection` | `10.0.9` | Registro dos serviços da camada de aplicação. |

### Infrastructure

| Biblioteca | Versão | Finalidade |
| --- | --- | --- |
| `Microsoft.EntityFrameworkCore` | `10.0.9` | Persistência e mapeamento objeto-relacional. |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | `10.0.2` | Provider PostgreSQL para EF Core. |

### Testes

| Biblioteca | Versão | Finalidade |
| --- | --- | --- |
| `xunit` | `2.9.3` | Framework de testes. |
| `xunit.runner.visualstudio` | `3.1.4` | Runner de testes para .NET/Visual Studio. |
| `FluentAssertions` | `6.12.0` | Asserções mais legíveis nos testes. |
| `Bogus` | `35.6.5` | Geração de dados fake para testes. |
| `coverlet.collector` | `6.0.4` | Coleta de cobertura de testes. |
| `Microsoft.NET.Test.Sdk` | `17.14.1` | SDK de testes do .NET. |

## Como rodar

### Pré-requisitos

Antes de iniciar, instale:

- [.NET SDK 10](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- Ferramenta do Entity Framework Core:

```bash
dotnet tool install --global dotnet-ef
```

Caso a ferramenta já esteja instalada, atualize com:

```bash
dotnet tool update --global dotnet-ef
```

### 1. Clone o repositório

```bash
git clone <url-do-repositório>
cd CashFlow
```

### 2. Suba o PostgreSQL

```bash
docker compose up -d
```

O banco local será iniciado com as credenciais definidas em `docker-compose.yml`:

| Campo | Valor |
| --- | --- |
| Host | `localhost` |
| Porta | `5432` |
| Database | `cashflow` |
| Usuário | `cashflow` |
| Senha | `cashflow123` |

### 3. Restaure as dependências

```bash
dotnet restore CashFlow.slnx
```

### 4. Aplique as migrations

```bash
dotnet ef database update --project src/CashFlow.Infrastructure --startup-project src/CashFlow.API
```

### 5. Rode a API

```bash
dotnet run --project src/CashFlow.API
```

Em ambiente de desenvolvimento, a API expõe o Swagger em:

```text
https://localhost:7265/swagger
http://localhost:5228/swagger
```

## Endpoints

### Despesas

| Método | Rota | Descrição |
| --- | --- | --- |
| `POST` | `/api/Expenses` | Cadastra uma nova despesa. |
| `GET` | `/api/Expenses` | Lista todas as despesas. |
| `GET` | `/api/Expenses/{id}` | Busca uma despesa pelo ID. |
| `PUT` | `/api/Expenses/{id}` | Atualiza uma despesa. |
| `DELETE` | `/api/Expenses/{id}` | Remove uma despesa. |

Exemplo de corpo para cadastro/atualização:

```json
{
  "title": "Mercado",
  "description": "Compra mensal",
  "date": "2026-07-15T10:00:00",
  "amount": 250.75,
  "paymentType": 1
}
```

Tipos de pagamento:

| Valor | Tipo |
| --- | --- |
| `0` | `Cash` |
| `1` | `CreditCard` |
| `2` | `DebitCard` |
| `3` | `EletronicTransfer` |

### Relatórios

| Método | Rota | Parâmetro | Descrição |
| --- | --- | --- | --- |
| `GET` | `/api/Report/excel` | Header `month` | Gera relatório mensal em Excel. |
| `GET` | `/api/Report/pdf` | Query `month` | Gera relatório mensal em PDF. |

Exemplo para PDF:

```text
GET /api/Report/pdf?month=2026-07-01
```

## Testes

Para executar os testes automatizados:

```bash
dotnet test CashFlow.slnx
```

Para executar com coleta de cobertura:

```bash
dotnet test CashFlow.slnx --collect:"XPlat Code Coverage"
```

## Estrutura de pastas

```text
CashFlow/
├── src/
│   ├── CashFlow.API/
│   ├── CashFlow.Application/
│   ├── CashFlow.Communication/
│   ├── CashFlow.Domain/
│   ├── CashFlow.Exception/
│   └── CashFlow.Infrastructure/
├── tests/
│   ├── CashFlow.CommonTestUtilities/
│   └── CashFlow.Tests/
├── docker-compose.yml
├── CashFlow.slnx
└── README.md
```

## Observações

- A connection string de desenvolvimento fica em `src/CashFlow.API/appsettings.Development.json`.
- As migrations ficam em `src/CashFlow.Infrastructure/Migrations`.
- O Swagger é habilitado apenas quando `ASPNETCORE_ENVIRONMENT=Development`.
- As pastas `bin/`, `obj/`, resultados de teste e arquivos locais de IDE são ignorados pelo Git.
