# 🎮 FIAP Cloud Games

Este projeto é parte do **Tech Challenge** da FIAP, com o objetivo de desenvolver uma plataforma de venda de jogos digitais e gestão de partidas online. Nesta fase, foi implementada uma **API RESTful em .NET 8** para gerenciamento de **usuários**, **jogos** e **promoções**.

---

## ✅ FUNCIONALIDADES

### 👤 Usuários

- Cadastro de novos usuários
- Listagem (todos ou por ID)
- Edição de informações
- Validação de inputs
- Autenticação com **JWT**
- Níveis de acesso:
  - **Usuário**
  - **Administrador**

### 🎮 Jogos
- Cadastro de jogos
- Validação de inputs
- Listagem (todos ou por ID)
- Exclusão de jogos
- Edição de informações
- Associação de jogos aos usuários

### 💸 Promoções
- Cadastro de promoções
- Validação de inputs
- Listagem (todas ou por ID)
- Edição de informações
- Exclusão de promoções

---

## 🧱 ARQUITETURA

O projeto segue os princípios da Clean Architecture, com uma separação clara entre as camadas:
- Domínio (entidades e regras de negócio)
- Aplicação (casos de uso)
- Infraestrutura (acesso a dados, serviços externos)
- Interface (API) (endpoints e middlewares)

Outros destaques:
- Projeto estruturado em formato monolítico
- Uso do **Entity Framework Core**, com suporte a migrations
- Middleware customizado para:
    - Tratamento global de erros
    - Geração de **logs estruturados**

---

## 🔧 TECNOLOGIAS

| Tecnologia         | Finalidade                                   |
|--------------------|----------------------------------------------|
| .NET 8             | Plataforma principal de desenvolvimento      |
| Entity Framework   | Mapeamento e persistência de dados           |
| xUnit              | Testes unitários                             |
| Swagger            | Documentação interativa da API               |
| JWT                | Autenticação baseada em token                |
| FluentValidation   | Validação robusta e desacoplada de modelos   |


---

## 🚀 EXECUTANDO O PROJETO LOCALMENTE

1. Clone o repositório:
   
   ```bash
   git clone https://github.com/PeterHSS/FIAPCloudGames.git
   cd FIAPCloudGames
2. Restaure os pacotes:
    ```bash
    dotnet restore
3. Aplique as migrations (caso ainda não esteja com o banco pronto):
    ```bash
    dotnet ef database update
4. Execute a API:
    ```bash
    dotnet run --project src/FIAPCloudGames
5. Acesse a documentação Swagger:
    ```bash
    https://localhost:{porta}/swagger
## 🧪 RODANDO OS TESTES
Execute os testes com o comando:
```bash
dotnet test
