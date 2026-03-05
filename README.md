## GamerSpace

Aplicação full stack para um e-commerce de periféricos gamer, com catálogo de produtos, autenticação de usuários, carrinho/checkout e área administrativa.

Pensado para servir como projeto de portfólio, demonstrando boas práticas em .NET (clean architecture) e Angular.

---

## Visão geral

- **Catálogo de produtos**: listagem de produtos com detalhes e variantes.
- **Categorias e classificações**: organização do catálogo por categorias e tipos de classificação.
- **Autenticação**: registro e login de usuários.
- **Carrinho e checkout**: criação de pedidos a partir do carrinho (checkout autenticado).
- **Área admin**: gerenciamento de produtos (listagem, criação, edição e remoção).

---

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
- `/products/:id` – detalhe do produto.
- `/cart` – carrinho.
- `/login` e `/register` – autenticação.
- `/admin/...` – área administrativa.

---

## Tecnologias

- **Backend**
  - .NET / ASP.NET Core
  - (EF Core ou similar para persistência, conforme configuração do projeto)
  - Autenticação baseada em token/JWT (conforme implementação da camada de aplicação)

- **Frontend**
  - Angular
  - TypeScript
  - SCSS

---

## Como rodar localmente

### Pré-requisitos

- .NET SDK instalado (versão compatível com o projeto).
- Node.js + npm instalados.

### Backend (API)

No diretório raiz do repositório:

```bash
cd Backend
dotnet restore
dotnet run --project GamerSpace.API/GamerSpace.API.csproj
```

A API deverá subir em uma porta configurada no projeto (por exemplo, `https://localhost:5001`/`http://localhost:5000`).

### Frontend (Angular)

Em outro terminal, a partir da raiz do repositório:

```bash
cd GamerSpace.UI
npm install
npm start
```

A aplicação Angular deverá ficar disponível em algo como `http://localhost:4200`.

Certifique-se de que a URL da API configurada no frontend aponta para o endereço em que o backend está rodando.

---

## English summary (short)

GamerSpace is a full stack application for an Gaming Gear e-commerce, built with **ASP.NET Core** and **Angular**.  
It showcases:

- a layered backend architecture (API, Application, Domain, Infrastructure)
- a feature-based Angular frontend (products, cart, auth, admin)
- typical e‑commerce flows: product catalog, authentication, cart and checkout and admin product management.
