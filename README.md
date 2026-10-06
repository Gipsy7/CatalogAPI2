# CatalogAPI2

API de catálogo de produtos e categorias usando **Minimal APIs do .NET 6**, Entity Framework Core sobre MySQL e autenticação **JWT**.

> Projeto de estudo (abril/2022). É a terceira versão da série de APIs de catálogo, depois do [APICatalogo](https://github.com/Gipsy7/APICatalogo) e do [CatalogAPI](https://github.com/Gipsy7/CatalogAPI).

## Destaques

- Endpoints organizados em **métodos de extensão** por recurso (`MapCategoriesEndpoints`, `MapProductsEndpoints`, `MapAuthenticationEndpoints`)
- Configuração do `Program.cs` quebrada em extensões (`AddPersistance`, `AddAuthenticationJwt`, `AddApiSwagger`, `UseAppCors`)
- Mapeamento do modelo com **Fluent API** no `OnModelCreating` (tamanhos, precisão do preço, relacionamento 1:N)
- Geração de token JWT (HMAC-SHA256, validade de 2 horas) e Swagger configurado para enviar o `Bearer`
- CORS liberado apenas para `GET`

## Endpoints

| Método | Rota | Autenticação | Descrição |
| --- | --- | --- | --- |
| POST | `/login` | — | Gera o token JWT |
| GET | `/categories` | JWT | Lista as categorias |
| GET | `/categories/{id}` | — | Busca uma categoria |
| POST | `/categories` | — | Cria uma categoria |
| PUT | `/categories/{id}` | — | Atualiza uma categoria |
| DELETE | `/categories/{id}` | — | Remove uma categoria |
| GET | `/products` | JWT | Lista os produtos |
| GET | `/products/{id}` | — | Busca um produto |
| POST | `/products` | — | Cria um produto |
| PUT | `/products/{id}` | — | Atualiza um produto |
| DELETE | `/products/{id}` | — | Remove um produto |

O login usa um usuário fixo de demonstração (`gipsy` / `1234`), definido em `EndPoints/AuthenticationEndPoints.cs`.

## Tecnologias

- .NET 6 / ASP.NET Core Minimal APIs
- Entity Framework Core 6 com Pomelo (MySQL)
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- Swashbuckle (Swagger)

## Como executar

Pré-requisitos: SDK do .NET 6 e um MySQL rodando.

1. Em `CatalogAPI2/appsettings.json`, ajuste a connection string `DefaultConnection` e a seção `Jwt` (`Key`, `Issuer`, `Audience`).
2. Crie o banco:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update --project CatalogAPI2
   ```
3. Rode a API e abra o Swagger em `/swagger`:
   ```bash
   dotnet run --project CatalogAPI2
   ```
4. Chame `POST /login`, copie o token e use o botão **Authorize** do Swagger com `Bearer <token>`.

---

Feito por **Mikael Francisco** · [Portfólio](https://mikaelfrancisco.vercel.app) · [LinkedIn](https://www.linkedin.com/in/mikael-francisco-a4300b180)
