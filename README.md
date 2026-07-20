# PetShop

Sistema de petshop desenvolvido como projeto final da disciplina de backend. Composto por uma API em **ASP.NET Core 10** (.NET) e um front-end em **Angular 22**.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (versão compatível com o npm usado no projeto)
- Um servidor **MySQL** acessível localmente (o projeto não utiliza containers)

## Banco de dados (MySQL local)

A aplicação espera um MySQL em execução e acessível pela string de conexão definida em `backend/appsettings.json` (seção `ConnectionStrings:DefaultConnection`).

Por padrão, o projeto usa:

- Servidor: `localhost:3306`
- Banco: `petshop`
- Usuário/Senha: configurados em `backend/appsettings.json`

> Ajuste a string de conexão em `backend/appsettings.json` conforme o seu ambiente MySQL antes de executar a API. O esquema do banco é criado via Entity Framework Migrations na inicialização da aplicação.

Opcionalmente, você pode popular dados de referência executando o script `backend/seed.sql` no banco `petshop`.

## Como rodar

### 1. Backend (API)

```sh
cd backend
dotnet restore
dotnet run
```

A API sobe em `http://localhost:5132` e os endpoints ficam em `http://localhost:5132/api` (rotas: `/api/auth`, `/api/animais`, `/api/tutores`, `/api/usuarios`).

Na inicialização, o `DataSeeder` cria automaticamente um usuário administrador caso não exista. A documentação OpenAPI/Swagger está disponível em ambiente de desenvolvimento.

### 2. Front-end (Angular)

```sh
cd frontend
npm install
npm start
```

O front-end é servido em `http://localhost:4200` e consome a API em `http://localhost:5132/api` (ver `frontend/src/environments/environment*.ts`).

## Para acessar

Com a tela de login aberta, use as credenciais de administrador (criadas automaticamente pelo seed):

- Usuário: `admin`
- Senha: `admin`

Não tem conta? Use "Criar conta de gestor" para registrar um novo usuário de acesso.
