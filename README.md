## VISÃO GERAL


Sistema de controle de gastos residenciais com:

-   **Backend:** .NET 8 / C#
-   **Frontend:** React + TypeScript

## Funcionalidades

-   Cadastro de pessoas (criar, listar, deletar)
-   Cadastro de categorias (criar, listar, deletar)
-   Cadastro de transações (criar, listar)
-   Dashboard com totais por pessoa
-   Dashboard com totais por categoria

## Regras de Negócio

-   Menores de idade só podem registrar **despesas**
-   Categorias respeitam a finalidade: **Despesa, Receita ou Ambas**
-   Ao deletar uma pessoa, todas as suas transações são removidas

----------

# Estrutura de Projetos

src/  
├─ API/                  # Backend  
│  ├─ Controllers/       # Endpoints  
│  ├─ DTOs/              # Data Transfer Objects  
│  ├─ Program.cs  
│  └─ ...  
│  
├─ Application/          # Lógica de negócio  
│  ├─ Services/  
│  ├─ Interfaces/  
│  ├─ DTOs/  
│  └─ ...  
│  
├─ Domain/               # Entidades e regras de negócio  
│  
├─ Infrastructure/       # Persistência (EF Core / SQLite / InMemory)  
│  
└─ Frontend/             # React + TypeScript  
 ├─ src/  
 │  ├─ pages/  
 │  │  ├─ Pessoas.tsx  
 │  │  ├─ Categorias.tsx  
 │  │  ├─ Transacoes.tsx  
 │  │  └─ Dashboard.tsx  
 │  │  
 │  ├─ api/  
 │  │  ├─ pessoas.ts  
 │  │  ├─ categorias.ts  
 │  │  ├─ transacoes.ts  
 │  │  └─ dashboard.ts  
 │  │  
 │  └─ types/  
 │     ├─ Pessoa.ts  
 │     ├─ Categoria.ts  
 │     └─ Transacao.ts  
 └─ ...

----------

# Backend

## Requisitos

-   .NET 8 SDK
-   SQLite (ou outro banco)
-   EF Core 8

## Setup Inicial

cd src/API  
dotnet restore  
dotnet ef migrations add InitialCreate  
dotnet ef database update  
dotnet run

Backend disponível em:  
**[http://localhost:5110](http://localhost:5110)**

----------

## Endpoints Disponíveis

### Pessoas

-   `GET /pessoas` → Lista todas
-   `POST /pessoas` → Cria
-   `DELETE /pessoas/{id}` → Remove (com transações)

### Categorias

-   `GET /categorias` → Lista
-   `POST /categorias` → Cria
-   `DELETE /categorias/{id}` → Remove
-   `GET /categorias/totais` → Totais por categoria

### Transações

-   `GET /transacoes` → Lista
-   `POST /transacoes` → Cria

### Dashboard

-   `GET /dashboard` → Totais por pessoa

----------

## Observações do Backend

-   `1 = Despesa`, `2 = Receita` (conversão feita no backend)
-   Categorias respeitam o campo **Finalidade**
-   Serviço de dashboard retorna:

{  
 "pessoas": [  
 {  
 "pessoaId": "...",  
 "nome": "...",  
 "totalReceitas": 0,  
 "totalDespesas": 0,  
 "saldo": 0  
 }  
 ],  
 "totaisGerais": {  
 "totalReceitas": 0,  
 "totalDespesas": 0,  
 "saldo": 0  
 }  
}

----------

# Frontend

## Setup Inicial

cd src/Frontend  
npm install  
npm run dev

Frontend disponível em:  
**[http://localhost:5173](http://localhost:5173)**

----------

## Estrutura

-   **api/** → consumo da API (PessoasApi, CategoriasApi, etc.)
-   **types/** → interfaces (Pessoa, Categoria, Transacao)
-   **pages/** → telas principais:
    -   Pessoas
    -   Categorias
    -   Transações
    -   Dashboard

----------

## Observações do Frontend

-   O dashboard atualiza automaticamente com:

window.dispatchEvent(new  Event("transacao-criada"))

-   Selects de pessoa e categoria carregam via API
-   Conversão de tipo de transação é feita no backend

----------

# Como Testar

1.  Inicie o backend:
    
    dotnet run
    
2.  Inicie o frontend:
    
    npm run dev
    
3.  Testes sugeridos:
    -   Criar pessoas (maior e menor de idade)
    -   Criar categorias com diferentes finalidades
    -   Criar transações
    -   Verificar atualização do dashboard
    -   Testar deleção de pessoas e categorias

----------

# Boas Práticas Aplicadas

-   Arquitetura limpa (**Clean Architecture**)
-   Separação de responsabilidades:
    -   **Domain:** regras e entidades
    -   **Application:** serviços e interfaces
    -   **Infrastructure:** persistência
    -   **API:** controllers e DTOs
-   Tipagem forte com TypeScript
-   Validações de regras de negócio:
    -   Menor de idade → apenas despesas
    -   Categoria compatível com transação
-   Dashboard reativo a eventos
-   Código documentado com comentários nos serviços
