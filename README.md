# Clean Architecture API

API desenvolvida em .NET 8.0 seguindo os princípios de Clean Architecture, CQRS e autenticação JWT.

## Requisitos

- .NET 8.0 SDK
- Docker e Docker Compose (para execução em containers)

## Execução com Docker Compose

1. Clone o repositório
2. Execute o comando: `docker-compose up --build`
3. A API estará disponível em: `https://localhost:5001`

## Endpoints

- POST `/api/users` - Cadastrar usuário
- GET `/api/users` - Listar usuários (requer autenticação)
- POST `/api/auth/login` - Autenticar usuário

## Exemplo de uso

### Cadastrar usuário:
```bash
curl -X POST "https://localhost:5001/api/users" \
  -H "Content-Type: application/json" \
  -d '{"name": "John Doe", "email": "john@example.com", "password": "Password123"}'