# banking-api-sample

Projeto exemplo de uma API de fintech/banco digital para gerenciar propostas de crédito.

## Estrutura
- `banking-api-sample.API` - ASP.NET Core Web API
- `banking-api-sample.Application` - Camada de aplicação (serviços, DTOs, validações, mapeamentos)
- `banking-api-sample.Domain` - Entidades, enums, interfaces de domínio
- `banking-api-sample.Infrastructure` - Persistência (EF Core), repositórios, configurações
- `banking-api-sample.Tests` - Testes unitários (xUnit)

## Requisitos
- .NET 9 SDK
- SQL Server (pode usar LocalDB ou container)
- (Opcional) dotnet-ef para gerenciar migrations

## Como rodar localmente
1. Restaurar pacotes
   - `dotnet restore`

2. Ajuste a connection string em `banking-api-sample.API/appsettings.json` (padrão usa LocalDB):
   ```json
   "ConnectionStrings": {
     "Default": "Server=(localdb)\\mssqllocaldb;Database=BankingDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. Aplicar migrações e criar banco (duas opções):
   - Usando EF tools (recomendado):
     - `dotnet tool install --global dotnet-ef` (se necessário)
     - `dotnet ef migrations add InitialCreate -p banking-api-sample.Infrastructure -s banking-api-sample.API`
     - `dotnet ef database update -p banking-api-sample.Infrastructure -s banking-api-sample.API`

   - Ou executar a API (o `Program` chama `Database.Migrate()` automaticamente):
     - `cd banking-api-sample.API`
     - `dotnet run`

4. A API estará disponível em `https://localhost:{port}` e o Swagger em `/swagger` (em ambiente de desenvolvimento).

## Endpoints principais
- `POST /proposals` - criar proposta
- `GET /proposals/{id}` - obter proposta por id
- `GET /proposals?status={Pending|UnderReview|Approved|Rejected}` - listar propostas (filtros)
- `POST /proposals/{id}/review` - mover para análise
- `POST /proposals/{id}/approve` - aprovar
- `POST /proposals/{id}/reject` - rejeitar

## Testes
- Rodar testes: `dotnet test`

## Publicar no GitHub (passos rápidos)
1. Inicialize um repositório Git local:
   - `git init`
   - `git add .`
   - `git commit -m "Initial commit: banking-api-sample"`

2. Crie repositório no GitHub e adicione remote:
   - `git remote add origin https://github.com/<seu-usuario>/<repo>.git`
   - `git push -u origin main`

3. Sugestão: adicione um `.gitignore` para .NET (visual studio) e um arquivo `LICENSE` se desejar.

## Observações
- As migrations iniciais foram adicionadas ao projeto de infraestrutura (`Infrastructure/Migrations`) para facilitar o uso sem executar ferramentas adicionais. Se preferir gerar suas próprias migrations, remova a pasta `Migrations/` e use `dotnet ef`.
- Para produção, ajuste configurações de connection string, logging e políticas de segurança.

---
Se quiser, eu posso também:
- adicionar um `.gitignore` e `LICENSE`;
- configurar uma action do GitHub para build e testes;
- remover a pasta `Migrations` e gerar as migrations localmente com `dotnet ef`.
