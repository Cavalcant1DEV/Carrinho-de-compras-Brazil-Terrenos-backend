# Carrinho-de-compras---Brasil-Terrenos
# Brasil Terrenos — API

API REST responsável pelo gerenciamento do catálogo, estoque, cupons e compras da aplicação Brasil Terrenos.

O projeto foi desenvolvido utilizando ASP.NET Core, Entity Framework Core e SQL Server, com foco em uma arquitetura simples, organizada e de fácil manutenção.

## Tecnologias

- .NET / ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Docker
- Docker Compose

## Funcionalidades

Atualmente a API possui suporte para:

- Listagem paginada de produtos
- Pesquisa de produtos por nome
- Consulta de estoque
- Cadastro de produtos
- Consulta e validação de cupons
- Criação de compras
- Aplicação de descontos
- Atualização de estoque após compras
- Registro de movimentações de estoque
- Migrations automáticas
- Seed inicial do banco de dados

## Estrutura do projeto

```text
api/
├── Controllers/
├── Data/
│   ├── Configurations/
│   └── Seeders/
├── DTOs/
│   ├── Common/
│   ├── Cupom/
│   ├── Error/
│   ├── Product/
│   └── Purchase/
├── Models/
├── Services/
├── Migrations/
├── Program.cs
└── appsettings.json
```

A aplicação segue, de forma geral, o seguinte fluxo:

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Entity Framework Core
   ↓
SQL Server
```

Os Controllers são responsáveis pela camada HTTP, enquanto as regras de negócio ficam concentradas nos Services.

## Banco de dados

O projeto utiliza SQL Server com Entity Framework Core.

As alterações no schema são controladas através de migrations.

Para criar uma nova migration:

```bash
dotnet ef migrations add NomeDaMigration
```

Para aplicar migrations manualmente:

```bash
dotnet ef database update
```

Durante a inicialização da aplicação, as migrations pendentes também são aplicadas automaticamente através de:

```csharp
await dbContext.Database.MigrateAsync();
```

## Executando o projeto

### Pré-requisitos

Para executar localmente:

- .NET 10
- Docker
- Docker Compose

Clone o repositório:

```bash
git clone https://github.com/Cavalcant1DEV/Carrinho-de-compras-Brazil-Terrenos-backend.git
cd <NOME_DO_PROJETO>
```

Configure as variáveis de ambiente necessárias.

Exemplo:

```env
DB_PASSWORD=SuaS3nha!
```

Depois execute:

```bash
docker compose up --build
```

A API ficará disponível em:

```text
http://localhost:8080
```

## Swagger

Em ambiente de desenvolvimento, a documentação da API pode ser acessada através do Swagger.

```text
http://localhost:8080/swagger
```

Através dele é possível visualizar os endpoints disponíveis, seus parâmetros, modelos de request e response e executar requisições diretamente pela interface.

## Principais endpoints

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/product` | Lista e pesquisa produtos |
| POST | `/api/product` | Cadastra um produto |
| GET | `/api/cupom` | Consulta um cupom |
| POST | `/api/purchase` | Realiza uma compra |

> Consulte o Swagger para visualizar o contrato completo de cada endpoint.

## Paginação

A listagem de produtos suporta paginação através de query parameters.

Exemplo:

```http
GET /api/product?page=1&pageSize=12
```

Também é possível pesquisar produtos:

```http
GET /api/product?page=1&pageSize=12&name=notebook
```

A resposta segue uma estrutura semelhante a:

```json
{
  "data": [],
  "page": 1,
  "pageSize": 12,
  "totalItems": 0,
  "totalPages": 0
}
```

## Compras

Durante a criação de uma compra, a API:

1. Valida os produtos solicitados
2. Valida as quantidades
3. Verifica o estoque disponível
4. Valida o cupom, quando informado
5. Calcula subtotal e desconto
6. Registra a compra
7. Atualiza o estoque
8. Registra as movimentações de estoque

Exemplo de request:

```json
{
  "products": [
    {
      "id": 1,
      "amount": 2
    }
  ],
  "discountId": 1
}
```

O campo `discountId` é opcional.

## Frontend

A interface web deste projeto está disponível em:

[Brasil Terrenos Frontend](https://github.com/Cavalcant1DEV/Carrinho-de-compras-Brazil-Terrenos-frontend.git)

## Licença

Este projeto foi desenvolvido para fins de estudo e demonstração técnica.

Camadas de autenticação e validações adicionais descartadas visto o escopo do desafio técnico informado abaixo:

## Contexto
Este teste tem como objetivo avaliar como você estrutura, modela e implementa uma solução back-end e front-end, aplicando boas práticas de separação de responsabilidades e regras de negócio claras. A entrega é estimada em até 2 dias. Não esperamos uma solução "perfeita" — o foco é um código limpo, organizado e funcional, com decisões de design que você consiga explicar e justificar. Objetivo Criar uma API que simule o funcionamento de um carrinho de compras, junto de um front-end que a consuma. 

## Requisitos funcionais
## - 1. Itens do carrinho 
  • Adicionar um produto ao carrinho (produto + quantidade)
  • Se o produto ainda não está no carrinho, ele entra no carrinho com quantidade 1. 
  • Se o produto já está no carrinho, a quantidade informada é somada à quantidade já existente (ex: carrinho tem 1 unidade, adiciona +1 → fica com 2).
  • Em ambos os casos, o preço do item (preço unitário × quantidade) deve ser recalculado e refletido na resposta.
  • Remover um produto do carrinho.
  • Se o produto não existir no carrinho, retorne um erro tratado.
  • Após a remoção, subtotal, desconto e total devem ser recalculados automaticamente.
  • Alterar a quantidade de um produto já existente no carrinho (substituição): define a quantidade exata do item, podendo aumentar ou diminuir o valor anterior (ex: item está com 2 unidades, altera para 5, depois para 7, etc.). O preço do item também deve ser recalculado de acordo com a nova quantidade.
 • Não deve ser possível adicionar ou definir uma quantidade menor ou igual a zero.
 • Estoque: cada produto possui uma quantidade disponível em estoque.
 • A API não deve permitir adicionar ao carrinho ou alterar a quantidade de um item para um valor maior do que o disponível em estoque — retorne um erro tratado nesse caso. • As respostas da API (consulta de produto e/ou item do carrinho) devem expor o preço líquido unitário e a quantidade disponível em estoque do produto.
## - 2. Catálogo de produtos
  • É fornecido o arquivo produtos.json com a lista de produtos disponíveis (10 itens), contendo os campos:
    • id
    • descricaoProduto 
    • quantidadeEstoque
    • precoLiquido
O candidato deve persistir esse catálogo no banco de dados (ex: via seed/migration), mantendo os mesmos valores, tipos e nomenclaturas apropriadas para o banco — por exemplo, colunas ID, DescricaoProduto, PrecoLiquido, QuantidadeEstoque, etc. — e utilizá-lo como base para os produtos referenciados nos demais endpoints do carrinho (adição, alteração de quantidade, validação de estoque, etc.). 
## - 3. Cupom de desconto 
  • Deve ser possível aplicar um cupom ao carrinho. 
  • É fornecido o arquivo cupons.json com os cupons disponíveis, contendo os campos: id codigoCupom percentualDesconto 
  • Devem ser implementados dois cupons: 
  • 10OFF → aplica 10% de desconto sobre o subtotal. 15OFF → aplica 15% de desconto sobre o subtotal. Os mesmos dados do cupons.json devem ser persistidos no banco de dados (ex: via seed/migration), mantendo os mesmos valores, tipos e nomenclaturas apropriadas para o banco — por exemplo, colunas ID, CodigoCupom, PercentualDesconto, etc. 
  • Regras a considerar: 
    • Apenas um cupom ativo por vez no carrinho — ter dois cupons cadastrados permite validar a troca de um pelo outro (ex: aplicar 10OFF, depois 15OFF, e verificar que apenas o segundo permanece ativo). 
    • Um cupom inválido/inexistente deve retornar erro tratado (não deve quebrar a aplicação). 
    • É permitido remover o cupom aplicado. 
## - 4. Cálculos 
- O carrinho deve ser capaz de calcular e expor: 
  • Subtotal: soma de (preço unitário × quantidade) de todos os itens.
  • Desconto: valor calculado com base no cupom aplicado (se houver).
  • Total: subtotal − desconto. Esses valores devem ser recalculados automaticamente sempre que o carrinho for alterado (adição, remoção, alteração de quantidade ou cupom).
## - 5. Finalizar carrinho 
  • O carrinho deve possuir um identificador (id) e um status (ex: Aberto / Finalizado), usado para controlar se ele ainda pode ser alterado. 
  • Além disso, o carrinho deve conter: 
    • Itens (produto, quantidade e preço do item). 
    • Cupom aplicado (se houver). 
    • Subtotal, desconto e total. 
    • Persistência do carrinho: crie uma tabela Carrinho no banco de dados para persistir os dados.
    • Deve existir uma operação para finalizar o carrinho (checkout). 
    • Um carrinho finalizado não pode mais ser alterado (não é possível adicionar/remover itens, alterar quantidades ou cupom). 
    • Tentativas de alteração em um carrinho finalizado devem retornar um erro tratado, com mensagem clara. 
## - Requisitos técnicos 
    • Linguagem/Framework (back-end): à sua escolha, dentre as três opções de stack abaixo. 
    • Tipo de projeto: API REST. 
    • Persistência: obrigatória em banco de dados (PostgreSQL ou SQL Server) — não há opção in-memory nesta etapa. 
    • Front-end: obrigatório, na tecnologia de sua preferência (React, Vue, Angular, Next.js, etc.), consumindo a API construída. Opção 1 — C# / .NET • .NET 8 ou superior, com ASP.NET Core (Web API ou Minimal API). 
    • Persistência: PostgreSQL ou SQL Server via Entity Framework Core. Opção 2 — Node.js / TypeScript 
    • Node.js com TypeScript, usando Express (ou framework equivalente). 
    • Persistência: PostgreSQL ou SQL Server via Prisma ORM. Opção 3 — Python 
    • Python, usando FastAPI (ou Flask), podendo aplicar Asyncio onde fizer sentido. 
    • Persistência: PostgreSQL ou SQL Server via SQLAlchemy (ou ORM equivalente).
