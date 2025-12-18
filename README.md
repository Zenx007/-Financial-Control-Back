# 📊 Financial Control API

API REST para controle financeiro desenvolvida com foco em **Clean Code**, **boas práticas de arquitetura** e **separação clara de responsabilidades**.  
O projeto segue uma abordagem em camadas utilizando **Controller**, **Service** e **Repository**, garantindo um código limpo, testável e escalável.

---

## 🎯 Objetivo do Projeto

O objetivo deste projeto é fornecer um backend robusto para controle financeiro, permitindo:

- Gerenciamento de usuários
- Cadastro de categorias financeiras
- Registro de receitas e despesas
- Consulta e organização das informações financeiras
- Base sólida para expansão futura (relatórios, dashboards, autenticação, etc.)

---

## 🏛 Arquitetura

O projeto foi estruturado seguindo princípios de **Clean Architecture**, onde as regras de negócio ficam isoladas de detalhes de infraestrutura.

### Fluxo de Responsabilidade

## 📂 Estrutura do Projeto

```text
📂 -Financial-Control-Back
├── 📂 FinancialControl.API             # Web API, Controllers, Program.cs
├── 📂 FinancialControl.Communication   # ViewObjects, Enums, API Response
├── 📂 FinancialControl.Core            # Entidades, Interfaces, Profiles
├── 📂 FinancialControl.Helpers         # Constantes, Métodos Estáticos
├── 📂 FinancialControl.Infrastructure  # DbContext, Migrations, Repositories Impl
└── 📄 FinancialControl.slnx            # Arquivo da Solução
```
---

## 🧩 Camadas do Sistema

### 🔹 Controllers (`FinancialControl.API`)

Camada responsável por lidar com requisições HTTP.

**Responsabilidades:**

- Definir endpoints
- Validar dados de entrada
- Retornar respostas HTTP
- Encaminhar requisições para os Services

> Controllers **não contêm regras de negócio**.

---

### 🔹 Services (`FinancialControl.Core`)

Camada responsável pelas **regras de negócio**.

**Responsabilidades:**

- Aplicar validações de domínio
- Coordenar fluxos da aplicação
- Garantir consistência dos dados
- Orquestrar chamadas aos repositórios

> Toda lógica de negócio vive nesta camada.

---

### 🔹 Repositories (`FinancialControl.Core` / `Infrastructure`)

Responsável pelo acesso a dados.

**Responsabilidades:**

- Persistência e leitura de dados
- Abstração do banco de dados
- Implementação concreta na Infrastructure

> O domínio depende de **interfaces**, nunca de implementações.

---

### 🔹 DTOs (`FinancialControl.Communication`)

Responsável pela comunicação entre API e Service.

**Vantagens:**

- Evita exposição das entidades de domínio
- Facilita versionamento da API
- Garante segurança e clareza nos contratos

---

### 🔹 Helpers (`FinancialControl.Helpers`)

Camada utilitária compartilhada.

**Exemplos:**

- Conversores e mapeadores
- Helpers de data e formatação
- Validações genéricas
- Tratamento de erros

---

## 🧱 Padrões e Princípios Utilizados

- **Clean Code**
- **Single Responsibility Principle (SRP)**
- **Dependency Injection**
- **Repository Pattern**
- **Service Layer Pattern**
- **Inversão de Dependência (DIP)**

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Finalidade |
|----------|-----------|
| .NET | Framework principal |
| C# | Linguagem |
| REST API | Comunicação |
| Entity Framework (ou similar) | Persistência |
| Dependency Injection | Desacoplamento |

---

## ⚙️ Configuração do Ambiente

### Pré-requisitos

- .NET SDK
- Banco de dados configurado
- IDE de sua preferência

