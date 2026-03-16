# GamerSpace

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](/LICENSE)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=DerickPipoca_GamerSpace-Project&metric=alert_status&token=4ad891dd529acd271903ed9c589ad2391d461aa9)](https://sonarcloud.io/summary/new_code?id=DerickPipoca_GamerSpace-Project)

![DotNet](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)
![MySQL](https://img.shields.io/badge/mysql-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)

---

## Conteúdo

- [Sobre o Projeto](#sobre-o-projeto)
- [Pré requisitos](#pré-requisitos)
- [Instalação e Execução](#instalação-e-execução)
- [Roadmap](#roadmap)
- [English summary](#english-summary)
- [Autor](#autor)

## Sobre o projeto

<p align="justify"> GamerSpace é um projeto fullstack de um e-commerce para periféricos 'Gamer'. Feito em camadas, clean architecture e DDD.
<br> 
Pensado para servir como projeto de portfólio, com boas práticas em .NET e Angular.
</p>

### Tecnologias usadas

- **Linguagem e Framework:**
  - Back-end/API:
    - [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
    - [ASP.NET Core 9 (Web API)](https://dotnet.microsoft.com/pt-br/)
  - Front-end:
    - [Angular 20](https://angular.dev/)
    - [TypeScript](https://www.typescriptlang.org/)
    - [Sass](https://sass-lang.com/)
- **Banco de Dados:**
  - [MySQL](https://www.mysql.com/)
  - [Entity Framework Core 9](https://learn.microsoft.com/pt-br/ef/core/) - NuGet [Pomelo](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- **Bibliotecas e Ferramentas:**
  - [JWT (JSON Web Tokens)](https://jwt.io/) - Autenticação
  - [AutoMapper](https://automapper.org/) - Mapeamento de Objetos
  - [FluentValidation](https://fluentvalidation.net/) - Validação
  - [Swagger](https://swagger.io/) - Documentação da API

### Imagens

<details>
<summary>Landing Page</summary>
<img src="./docs/landing-page.png" alt="Landing Page" width="300">
</details>
<details>
<summary>Página de produto</summary>
<img src="./docs/product.png" alt="Product" width="300">
</details>
<details>
<summary>Carrinho</summary>
<img src="./docs/cart.png" alt="Cart" width="300">
</details>
<details>
<summary>Login</summary>
<img src="./docs/login.png" alt="Login" width="300">
</details>
<details>
<summary>Register</summary>
<img src="./docs/register.png" alt="Register" width="300">
</details>

## Arquitetura

### Backend (.NET)

Solução `GamerSpace.sln` organizada em camadas:

- **GamerSpace.API**  
  API HTTP (ASP.NET Core) que expõe endpoints para:
  - produtos e variantes
  - categorias e tipos de classificação
  - autenticação (registro/login)
  - pedidos/checkout

- **GamerSpace.Application**  
  Casos de uso e lógica de aplicação (commands/queries, DTOs).

- **GamerSpace.Domain**  
  Modelo de domínio (entidades, regras de negócio).

- **GamerSpace.Infrastructure**  
  Acesso a dados e integrações externas (repositórios, contexto de banco, etc.).

### Frontend (Angular)

Aplicação Angular na pasta `GamerSpace.UI`, estruturada em:

- **core**: serviços centrais, guards, configuração.
- **features**: módulos de funcionalidade (produtos, carrinho, auth, admin, etc.).
- **layout/shared**: componentes reutilizáveis e layout (como o `product-card`).

Rotas principais (exemplos):

- `/home` – página inicial.
- `/products` – listagem de produtos.
- `/cart` – carrinho.
- `/login` e `/register` – autenticação.
- `/admin/...` – área administrativa.

## Pré requisitos

Antes de começar, garanta que você tem instalado o Docker:

- [Docker Desktop](https://docs.docker.com/desktop)

## Instalação e Execução

Siga os passos abaixo para ter um multi-container do docker rodando localmente em sua máquina:

### Via CLI (Development)

1. Clone o repositório.
2. As informações sensíveis devem ser configuradas via User Secrets. No terminal, na pasta do projeto, execute os comandos abaixo para configurar os segredos da aplicação:

```Bash
#Iniciar user-secrets
dotnet user-secrets init

#Configurar a Connection String do MySQL
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=gamerspacedb;Uid=root;Pwd=sua_senha;"

#Configurar a Key do JWT
dotnet user-secrets set "JWT:Key" "Senha-Longa1234567890-1234567890-1234567890"
```

3. Acesse a API em https://localhost:7247/swagger e o Frontend em http://localhost:4200.

### Via Docker (Production)

1. Clone o repositório.
2. Crie um arquivo `.env` na pasta raiz do projeto e siga o modelo do arquivo `.env.example` para configurar os user-secrets.
3. Na raiz do projeto, execute:
   ```bash
   docker-compose up --build
   ```
4. Acesse o projeto em http://localhost:4200.

## Roadmap

O projeto ainda não está 100% completo, mas o MVP está feito.

- [ ] Pagamento integrado com PagSeguro.
- [ ] Histórico de pedidos específico de cada usuário.
- [ ] Histórico de pedidos geral (ADMIN).
- [ ] Gerenciamento dos tipos de categorias (ADMIN).
- [ ] Alterar senha.
- [ ] Descrição dos itens em MD.

## English summary

A project for my portfolio named GamerSpace. It's a full stack application for an gaming gear e-commerce, built with **ASP.NET Core** and **Angular**.  
It showcases:

- A layered backend architecture `(API, Application, Domain, Infrastructure)`
- A feature-based Angular frontend `(products, cart, auth, admin)`
- Typical e‑commerce flows: product catalog, authentication, cart and checkout and admin product management.

## Autor

[@DerickPipoca](https://github.com/DerickPipoca)<a name = "autor"></a>