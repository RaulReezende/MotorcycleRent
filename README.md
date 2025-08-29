# Motorcycle Rent

## Descrição
Motorcycle rent é uma aplicação para o gerenciamento de locações de moto, onde é possível cadastrar entregadores e motos. A aplicação foi desenvolvida em .NET utilizando Clean Architecture, também utiliza PostgreSQL, MinIO e RabbitMq

## Como rodar

1. **Pré-requisitos:**  
   - Docker instalado
   - Docker Desktop inicializado

2. **Configuração:**  
   - Edite o arquivo `.env` com as variáveis de ambiente necessárias.

3. **Suba os serviços:**  
   - pelo Visual Studio rode o perfil docker-compose

4. **Acessar a API:**  
   - [http://localhost:8081](http://localhost:8081/)


## Arquitetura
- **Domain:** Entidades, interfaces e regras de negócio.
- **Application:** Serviços de aplicação, validações, DTOs, mapeamentos e regras de orquestração.
- **Infrastructure:** Repositórios, serviços de mensageria (RabbitMQ), file storage (MinIO) e banco de dados (PostgreSQL).
- **API:** Aplicação inicial, Controllers REST, middlewares e configuração de dependências.

## Tecnologias
- .NET 9
- RabbitMQ
- MinIO
- Docker
- PostgreSQL
