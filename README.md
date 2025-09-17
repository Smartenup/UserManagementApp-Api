# Clean Architecture API

API desenvolvida em .NET 8.0 seguindo os princ�pios de Clean Architecture, CQRS e autentica��o JWT.

## Requisitos

- .NET 8.0 SDK
- Docker e Docker Compose (para execu��o em containers)

## Execu��o com Docker Compose

1. Clone o reposit�rio
2. Execute o comando: `docker-compose up --build`
3. A API estar� dispon�vel em: `https://localhost:5001`

## Endpoints

- POST `/api/users` - Cadastrar usu�rio
- GET `/api/users` - Listar usu�rios (requer autentica��o)
- POST `/api/auth/login` - Autenticar usu�rio

## Exemplo de uso

### Cadastrar usu�rio:
```bash
curl -X POST "https://localhost:5001/api/users" \
  -H "Content-Type: application/json" \
  -d '{"name": "John Doe", "email": "john@example.com", "password": "Password123"}'