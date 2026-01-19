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

## 4. Variáveis de Ambiente
| Variável | Descrição | Exemplo |
| :--- | :--- | :--- |
| `RabbitMQ__Host` | Host do Broker de Mensageria | `rabbitmq://rabbitmq-service` |
| `Smtp__Provider` | Provedor de e-mail simulado | `ConsoleLogger` |
| `App__WelcomeTemplate` | Template da mensagem de boas-vindas | `Bem-vindo à FCG!` |

## 👥 Integrantes
- **Nome do Grupo:**: 33.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).
