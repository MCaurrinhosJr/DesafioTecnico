# GoodHamburger

A Good Hamburger API é uma aplicação RESTful que serve como backend para um restaurante fictício chamado "Good Hamburger". A API oferece funcionalidades para gerenciamento de menus e pedidos, permitindo que os clientes interajam com o sistema de maneira simples e eficiente. Ela foi construída utilizando o framework .NET 8.0, Entity Framework Core com SQL Server, e conta com uma documentação automática gerada pelo Swagger.

## Tecnologias Usadas
- **.NET 8.0**: Framework para desenvolvimento da aplicação.
- **Entity Framework Core**: ORM para manipulação do banco de dados.
- **SQL Server**: Banco de dados utilizado para armazenar informações.
- **Swagger**: Ferramenta para documentação automática da API.
- **ASP.NET Core**: Framework para construção da API.

## Funcionalidades
- **Menu Management**: Exibe os itens do menu disponíveis no restaurante.
- **Order Management**: Permite a criação, visualização, atualização e exclusão de pedidos.
- **Discount Calculation**: Calcula descontos aplicáveis aos pedidos.
- **Error Handling**: Customização no tratamento de erros através de middleware.

## Endpoints da API

### 1. Menu
**GET /api/menu**  
Retorna todos os itens disponíveis no menu.

### 2. Pedidos
- **GET /api/order**  
  Lista todos os pedidos realizados.
- **GET /api/order/{id}**  
  Retorna um pedido específico através do seu ID.
- **POST /api/order**  
  Cria um novo pedido.
- **PUT /api/order/{id}**  
  Atualiza um pedido existente pelo ID.
- **DELETE /api/order/{id}**  
  Remove um pedido específico pelo ID.

## Como Rodar o Projeto

### 1. Clonar o Repositório
Clone o repositório para sua máquina local:

```bash
git clone https://github.com/MCaurrinhosJr/DesafioTecnico.git
```

### 2. Restaurar Dependências
No diretório do projeto, execute o seguinte comando para restaurar as dependências:

```bash
dotnet restore
```

### 3. Configuração do Banco de Dados
- **SQL Server**: Certifique-se de que o SQL Server está instalado e configurado corretamente.
- **String de Conexão**: Configure a string de conexão no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GoodHamburgerDb;Trusted_Connection=True;"
  }
}
```

### 4. Aplicar Migrações
Para criar o banco de dados e as tabelas, execute as migrações com o comando:

```bash
dotnet ef database update
```

### 5. Executar o Projeto
Execute o comando abaixo para rodar a aplicação em modo de desenvolvimento:

```bash
dotnet run
```

A API estará acessível em [https://localhost:5001](https://localhost:5001).

### 6. Acessar a Documentação Swagger
A documentação da API gerada pelo Swagger pode ser acessada em:

[https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## Estrutura do Projeto

A aplicação segue uma arquitetura limpa e organizada em camadas:

- **GoodHamburger.WebApi**: Contém os controladores da API e configurações do middleware.
- **GoodHamburger.Core**: Contém as interfaces, modelos e lógica de negócios.
- **GoodHamburger.Infra**: Implementação do repositório e contexto do banco de dados.

---

## Injeção de Dependências

A injeção de dependência (DI) é configurada no `Program.cs`, onde todos os serviços e repositórios são registrados para facilitar a manutenção e os testes.

## Exemplo de Uso

### 1. Obter Itens do Menu

**Requisição:**

```http
GET /api/menu
```

**Resposta:**

```json
[
  {
    "id": 1,
    "name": "X Burger",
    "price": 5,
    "type": 0
  },
  {
    "id": 2,
    "name": "X Egg",
    "price": 4.5,
    "type": 0
  },
  {
    "id": 3,
    "name": "X Bacon",
    "price": 7,
    "type": 0
  },
  {
    "id": 4,
    "name": "Batata Frita",
    "price": 2,
    "type": 1
  },
  {
    "id": 5,
    "name": "Refrigerante",
    "price": 2.5,
    "type": 2
  }
]
```

### 2. Criar um Pedido

**Requisição:**

```http
POST /api/order
Content-Type: application/json
```

```json
{
  "items": [
    {
      "menuItemId": 1,
      "name": "X Burger",
      "price": 5.0,
      "type": 0
    },
    {
      "menuItemId": 4,
      "name": "Batata Frita",
      "price": 2.0,
      "type": 1
    },
    {
      "menuItemId": 5,
      "name": "Refrigerante",
      "price": 2.5,
      "type": 2
    }
  ]
}
```

**Resposta:**

```json
{
  "id": 1,
  "items": [
    {
      "menuItemId": 1,
      "name": "X Burger",
      "price": 5.0,
      "type": 0
    },
    {
      "menuItemId": 4,
      "name": "Batata Frita",
      "price": 2.0,
      "type": 1
    },
    {
      "menuItemId": 5,
      "name": "Refrigerante",
      "price": 2.5,
      "type": 2
    }
  ],
  "price": 9.5,
  "discount": 1.9,
  "totalPrice": 7.6
}
```

---

## Implementações Futuras

Abaixo estão algumas das implementações planejadas para o futuro da API Good Hamburger:

### 1. **Frontend Blazor**

- **Objetivo**: Criar uma interface de usuário interativa utilizando o framework Blazor, permitindo que os usuários façam pedidos diretamente no navegador.
- **Detalhes**: 
  - O Blazor será usado para criar uma aplicação web com interação em tempo real, consumindo os endpoints da API RESTful criada.
  - Haverá uma tela para visualização do cardápio, onde os itens serão exibidos dinâmicamente a partir da API.
  - A funcionalidade de criação de pedidos será implementada, com a interface permitindo a adição de sanduíches, acompanhamentos e bebidas.
  - Também será possível calcular o valor do pedido com os descontos aplicados e visualizar o resumo do pedido.
  
---
## Contribuindo

1. Faça o fork do repositório.
2. Crie uma nova branch:

```bash
git checkout -b feature/nome-da-feature
```

3. Faça as alterações necessárias e adicione os arquivos modificados:

```bash
git add .
```

4. Realize o commit das alterações:

```bash
git commit -m 'Adiciona nova funcionalidade'
```

5. Envie para o seu fork:

```bash
git push origin feature/nome-da-feature
```

6. Abra um pull request no repositório original.
