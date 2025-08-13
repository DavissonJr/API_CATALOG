# DEVLOOM_CATALOG API

![Dev Branch](https://img.shields.io/badge/branch-dev-blue)
![Main Branch](https://img.shields.io/badge/branch-main-green)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![API Version](https://img.shields.io/badge/version-1.0.0-yellow)

API de gerenciamento de **Categorias** e **Produtos** com suporte a CRUD e paginação.

---

## 🔹 Tecnologias Utilizadas

- **.NET 8**  
- **C#**  
- **Dapper** (para acesso a dados)  
- **AutoMapper** (para mapeamento de DTOs e Domain)  
- **SQL Server** (como banco de dados)  
- **Swagger / Swashbuckle** (para documentação e testes de API)  
- **Visual Studio 2022** ou **VS Code** (IDE)  
- **Padrões de arquitetura SOLID** aplicados no desenvolvimento

---

## 🔹 Ambientes

- **Dev**: ambiente de desenvolvimento. Todas as funcionalidades foram criadas e testadas aqui inicialmente.  
- **Main**: branch principal, onde o código foi mesclado após os testes em Dev estarem completos e aprovados.  

> O fluxo de desenvolvimento seguiu a prática de **feature branches**, garantindo que o main sempre esteja estável.

---

## 🔹 Estrutura do Projeto

DEVLOOM_CATALOG/
│
├─ Controllers/
│ ├─ CategoriaController.cs
│ └─ ProdutoController.cs
│
├─ Domain/
│ ├─ Categoria.cs
│ └─ Produto.cs
│
├─ Infra/
│ ├─ Interfaces/
│ │ ├─ ICategoriaRepository.cs
│ │ └─ IProdutoRepository.cs
│ └─ Repositories/
│ ├─ CategoriaRepository.cs
│ └─ ProdutoRepository.cs
│
├─ Services/
│ ├─ Interfaces/
│ │ ├─ ICategoriaService.cs
│ │ └─ IProdutoService.cs
│ └─ Service/
│ ├─ CategoriaService.cs
│ └─ ProdutoService.cs
│
├─ Application/
│ ├─ DTOs/
│ │ ├─ CategoriaRequestDto.cs
│ │ ├─ CategoriaResponseDto.cs
│ │ ├─ ProdutoRequestDto.cs
│ │ └─ ProdutoResponseDto.cs
│ └─ Mappings/
│ └─ CatalogoProfile.cs
│
├─ Program.cs
└─ DEVLOOM_CATALOG.csproj

## 🔹 Configuração e Execução

1. **Clonar o repositório:**

```bash
git clone <url-do-repositorio>
cd DEVLOOM_CATALOG
```

2. **Atualizar string de conexão no appsettings.json:**

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DEVLOOM_CATALOG;User Id=sa;Password=SuaSenha123;"
}
```

3. **Instalar pacotes NuGet (caso não estejam instalados):**
```bash
dotnet restore
```

4. **Rodar a aplicação:**
```bash
dotnet run
```

5. **Swagger:**
Acesse a documentação em:
```bash
https://localhost:<porta>/swagger/index.html
```

## 🔹 Endpoints da API

### Categorias

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET    | `/api/categoria` | Recupera todas as categorias |
| POST   | `/api/categoria` | Adiciona uma nova categoria |
| GET    | `/api/categoria/{id}` | Busca categoria por Id |
| PUT    | `/api/categoria/{id}` | Atualiza uma categoria existente |
| DELETE | `/api/categoria/{id}` | Deleta uma categoria |

### Produtos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET    | `/api/produto?categoriaId=<guid>&page=<int>&size=<int>` | Busca produtos de uma categoria com paginação obrigatória |
| POST   | `/api/produto` | Adiciona um produto |
| GET    | `/api/produto/{id}` | Busca produto por Id |
| PUT    | `/api/produto/{id}` | Atualiza um produto existente |
| DELETE | `/api/produto/{id}` | Deleta um produto |

---

## 🔹 Testando a API
- Utilize o **Swagger** para testar os endpoints.
- Ou use o **Postman / Insomnia** para requisições **HTTP**.

- Exemplo **JSON** para criar uma **categoria**:
```bash
{
  "nome": "Eletrônicos",
  "ativo": true
}
```

- Exemplo **JSON** para criar um **produto**:
```bash
{
  "nome": "Smartphone",
  "precoUnitario": 2500.50,
  "categoriaId": "GUID-da-categoria",
  "ativo": true
}
```

## 🔹 Scripts SQL

```bash
CREATE TABLE Categorias (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID(),
    Nome NVARCHAR(50) NOT NULL,
    DataCriacao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DataAtualizacao DATETIME2 NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Categorias PRIMARY KEY (Id),
    CONSTRAINT UQ_Categorias_Nome UNIQUE (Nome) 
);

CREATE TABLE Produtos (
    Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID(),
    Nome NVARCHAR(100) NOT NULL,
    PrecoUnitario DECIMAL(18,2) NOT NULL CHECK (PrecoUnitario >= 0),
    CategoriaId UNIQUEIDENTIFIER NOT NULL,
    DataCriacao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DataAtualizacao DATETIME2 NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Produtos PRIMARY KEY (Id),
    CONSTRAINT FK_Produtos_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Produtos_Nome_Categoria UNIQUE (CategoriaId, Nome) 
);

-- Índices extras para performance ( melhorando a páginação para muitos dados, alta escalabilidade )
CREATE NONCLUSTERED INDEX IX_Produtos_CategoriaId
ON Produtos (CategoriaId, Nome);
```

## 🔹 Observações
- O AutoMapper deve estar configurado com todos os Profiles registrados.
- O banco de dados deve estar disponível no SQL Server.
- Use dotnet clean + dotnet build após alterações nos pacotes ou DTOs.
- O código segue princípios SOLID para manutenibilidade e escalabilidade.

## 🔹 Autor
Dávisson Falcão
