# 🎮 FIAP Cloud Games - UsersAPI

Responsável pela comunicação com o usuário através do envio de e-mails (simulados via log) baseados em eventos do sistema.

## 1. Funcionalidades
* Envio de e-mail de boas-vindas para novos usuários.
* Envio de confirmação de compra de jogos.

## 2. Fluxo Orientado a Eventos
Este serviço é um consumidor puramente reativo.

* **Consumidos:**
    * `UserCreatedEvent`: Gatilho para o envio do e-mail de boas-vindas.
    * `PaymentProcessedEvent`: Se o status for `Approved`, dispara o e-mail de confirmação da compra com os detalhes do jogo.

## 3. Tecnologias
* **Linguagem:** .NET 10
* **Banco de Dados:** SQL Server
* **Mensageria:** RabbitMQ (via MassTransit)
* **Padrões:** MediatR, FluentValidation
* **Documentação:** Swagger
* **Orquestração:** Docker & Kubernetes

## 4. Configuração do Ambiente
Para que a aplicação funcione corretamente, edite o arquivo `appsettings.Development.json` seguindo o modelo abaixo:

```json
{
  "ConnectionStrings": {
    "ConnectionStrings": "Server=users-sqlserver;Initial Catalog=db_fcg_users;Persist Security Info=False;User ID=sa;Password=pass@123;Encrypt=False;Pooling=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "ChaveSuperSecretaComMaisDe32CaracteresAqui12345",
    "Issuer": "FCG-Users"
  },
  "Rabbitmq": {
    "Url": "localhost",
    "Username": "admin",
    "Password": "admin123"
  },
  "AllowedHosts": "*" 
}
```

## 👥 Integrantes
- **Nome do Grupo:**: 33.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).
