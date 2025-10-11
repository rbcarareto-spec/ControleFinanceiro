# 📊 Controle Financeiro

Sistema para controle de receitas e despesas de pequenas empresas, com
categorização de transações, dashboard financeiro e relatórios
avançados.

------------------------------------------------------------------------

## 🧰 Tecnologias Utilizadas

-   **.NET 8.0**
-   **C#**
-   **Bootstrap 5**
-   **Entity Framework Core**
-   **SQLite / SQL Server**

------------------------------------------------------------------------

## 🌟 Funcionalidades

### 📋 Cadastros

-   **Categorias**: Cadastro de categorias de receita e despesa (com
    status ativo/inativo)
-   **Transações**: Registro de receitas e despesas com data, valor,
    categoria e observações

### 📊 Relatórios

-   **Dashboard**: Visão geral com saldo, receitas e despesas
-   **Saldo por Período**: Relatório filtrável por data com exportação
    para Excel (.xlsx)

### 🔒 Validações

-   Valor da transação deve ser maior que zero\
-   Data da transação não pode ser futura\
-   Categoria deve existir e estar ativa\
-   Nome da categoria é obrigatório

### 🛠️ Outros

-   Interface responsiva com Bootstrap 5\
-   Mensagens de sucesso/erro elegantes\
-   Confirmação segura para exclusão\
-   Busca, paginação e filtros avançados (**DataTables.js**)

------------------------------------------------------------------------

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e separação de
responsabilidades:

    ControleFinanceiro.sln
    ┣ ControleFinanceiro.Web       → Camada de apresentação (MVC)
    ┣ ControleFinanceiro.Domain    → Entidades e enums
    ┣ ControleFinanceiro.Aplicacao → Serviços, DTOs, interfaces de repositório
    ┣ ControleFinanceiro.Infra     → Implementações de repositórios, configuração do EF Core

### Padrões Utilizados

-   Repository Pattern\
-   Service Layer\
-   DTO (Data Transfer Object)\
-   Fluent API para mapeamento do Entity Framework\
-   Injeção de Dependência

------------------------------------------------------------------------

## 🚀 Como Executar

### Pré-requisitos

-   .NET 8.0 SDK\
-   SQL Server (LocalDB, Express ou Azure SQL)

### Passo a passo

#### 1️⃣ Clone o repositório

``` bash
git clone https://github.com/seu-usuario/ControleFinanceiro.git
cd ControleFinanceiro
```

#### 2️⃣ Restaure os pacotes

``` bash
dotnet restore
```

#### 3️⃣ Configure a string de conexão

Edite o arquivo `ControleFinanceiro.Web/appsettings.json`:

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ControleFinanceiroDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### 4️⃣ Gere e aplique as migrations

``` bash
dotnet ef database update --project ControleFinanceiro.Infra --startup-project ControleFinanceiro.Web
```

#### 5️⃣ Execute a aplicação

``` bash
dotnet run --project ControleFinanceiro.Web
```

#### 6️⃣ Acesse no navegador

-   https://localhost:7000\
-   http://localhost:5000

------------------------------------------------------------------------

## 📦 Pacotes Principais

  --------------------------------------------------------------------------------------------
  Pacote                                    Versão                 Descrição
  ----------------------------------------- ---------------------- ---------------------------
  Microsoft.EntityFrameworkCore.SqlServer   8.0.x                  ORM para SQL Server

  Microsoft.EntityFrameworkCore.Design      8.0.x                  Ferramentas de migração

  EPPlus                                    7.1.3                  Geração de arquivos Excel

  Bootstrap                                 5.3.3                  Framework CSS

  Bootstrap Icons                           1.11.3                 Ícones vetoriais

  DataTables                                2.0.8                  Tabelas avançadas com
                                                                   filtros
  --------------------------------------------------------------------------------------------

------------------------------------------------------------------------

## 🖼️ Telas

### Dashboard

![Dashboard](docs/dashboard.png)

### Listagem de Transações com Filtros

![Transações](docs/transacoes.png)

### Relatório com Exportação Excel

![Relatório](docs/relatorio.png)

> As imagens acima são ilustrativas. Adicione screenshots reais na pasta
> `docs/`.

------------------------------------------------------------------------

## 📝 Licença

Este projeto é de código aberto sob a licença **MIT**.\
Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

------------------------------------------------------------------------

## 🤝 Contribuições

Contribuições são bem-vindas!\
Siga estas etapas:

1.  Faça um fork do projeto\

2.  Crie uma branch para sua feature

    ``` bash
    git checkout -b feature/nova-funcionalidade
    ```

3.  Commit suas mudanças

    ``` bash
    git commit -m 'Adiciona nova funcionalidade'
    ```

4.  Push para a branch

    ``` bash
    git push origin feature/nova-funcionalidade
    ```

5.  Abra um Pull Request

------------------------------------------------------------------------

## 📧 Contato

Desenvolvido com ❤️ por **\[Seu Nome\]**\
📧 \[seu.email@exemplo.com\]\
🔗 [LinkedIn](https://linkedin.com/in/seu-perfil) \|
[GitHub](https://github.com/seu-usuario)
