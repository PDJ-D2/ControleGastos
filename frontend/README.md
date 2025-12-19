Visão Geral

Sistema de controle de gastos residenciais com backend em .NET 8/C# e frontend em React + TypeScript.
Funcionalidades:

Cadastro de pessoas (criar, listar, deletar)

Cadastro de categorias (criar, listar, deletar)

Cadastro de transações (criar, listar)

Dashboard com totais por pessoa

Dashboard com totais por categoria

Regras de negócio:

Menores de idade só podem registrar despesas

Categorias respeitam a finalidade (despesa/receita/ambas)

Deletar pessoa apaga todas as transações associadas

Estrutura de Projetos
/TT
 ├─ src/
 │   ├─ API/               # Backend
 │   │   ├─ Controllers/   # Endpoints
 │   │   ├─ DTOs/          # Data Transfer Objects
 │   │   ├─ Program.cs
 │   │   └─ ...
 │   ├─ Application/       # Lógica de negócio / Services / Interfaces
 │   │   ├─ Services/
 │   │   ├─ Interfaces/
 │   │   ├─ DTOs/
 │   │   └─ ...
 │   ├─ Domain/            # Entidades, enums, regras de negócio
 │   ├─ Infrastructure/    # Repositórios, persistência (InMemory ou EF Core/SQLite)
 │   └─ Frontend/          # React + TS
 │       ├─ src/
 │       │   ├─ pages/
 │       │   │   ├─ Pessoas.tsx
 │       │   │   ├─ Categorias.tsx
 │       │   │   ├─ Transacoes.tsx
 │       │   │   └─ Dashboard.tsx
 │       │   ├─ api/
 │       │   │   ├─ pessoas.ts
 │       │   │   ├─ categorias.ts
 │       │   │   ├─ transacoes.ts
 │       │   │   └─ dashboard.ts
 │       │   └─ types/
 │       │       ├─ Pessoa.ts
 │       │       ├─ Categoria.ts
 │       │       └─ Transacao.ts
 └─ ...

Backend
Requisitos

.NET 8 SDK

SQLite (ou outro DB de sua escolha)

EF Core 8 (já configurado no projeto)

Setup Inicial

Navegue até a pasta API:

cd src/API


Restaurar pacotes:

dotnet restore


Criar/migrar banco de dados:

dotnet ef migrations add InitialCreate
dotnet ef database update


Rodar o backend:

dotnet run


O backend ficará disponível em http://localhost:5110.

Endpoints Disponíveis
Recurso	Método	Endpoint	Descrição
Pessoas	GET	/pessoas	Lista todas as pessoas
Pessoas	POST	/pessoas	Cria uma pessoa
Pessoas	DELETE	/pessoas/{id}	Deleta uma pessoa (transações também)
Categorias	GET	/categorias	Lista todas as categorias
Categorias	POST	/categorias	Cria uma categoria
Categorias	DELETE	/categorias/{id}	Deleta uma categoria
Categorias	GET	/categorias/totais	Totais por categoria
Transações	GET	/transacoes	Lista todas as transações
Transações	POST	/transacoes	Cria uma transação
Dashboard	GET	/dashboard	Totais por pessoa (receitas, despesas, saldo)
Observações Importantes

CriarTransacaoService converte 1 = Despesa e 2 = Receita.

TotaisPorPessoaService retorna o objeto compatível com o front:

{
  "pessoas": [
    { "pessoaId": "...", "nome": "...", "totalReceitas": 0, "totalDespesas": 0, "saldo": 0 }
  ],
  "totaisGerais": { "totalReceitas": 0, "totalDespesas": 0, "saldo": 0 }
}


Categorias respeitam o campo Finalidade (Despesa, Receita, Ambas).

Frontend
Setup Inicial

Navegue até a pasta Frontend:

cd src/Frontend


Instalar dependências:

npm install


Rodar frontend:

npm run dev


O frontend ficará disponível em http://localhost:5173.

Estrutura de Frontend

api/ → funções para consumir a WebAPI (PessoasApi, CategoriasApi, TransacoesApi, DashboardApi)

types/ → interfaces Pessoa, Categoria, Transacao

pages/ → telas principais: Pessoas, Categorias, Transações, Dashboard

Observações

Eventos window.dispatchEvent(new Event("transacao-criada")) atualizam o dashboard automaticamente quando você cria uma transação.

Conversão de tipo da transação (1 = despesa, 2 = receita) é feita no backend.

Todos os selects de pessoa/categoria carregam automaticamente via API.

Como Testar

Suba o backend (dotnet run)

Suba o frontend (npm run dev)

Crie algumas pessoas (maior e menor de idade)

Crie categorias com diferentes finalidades

Crie transações e veja o dashboard atualizar automaticamente

Teste deleção de pessoas e categorias

Boas Práticas Aplicadas

Arquitetura limpa (Clean Architecture)

Separação clara entre:

Domain: entidades, regras de negócio

Application: serviços e interfaces

Infrastructure: repositórios e persistência

API: controllers e DTOs

Tipagem forte no frontend (TypeScript)

Dashboard reagindo a eventos de criação de transações

Validação de regras de negócio:

Menor de idade → só despesas

Categoria compatível com tipo da transação

Comentários nos serviços explicando lógica