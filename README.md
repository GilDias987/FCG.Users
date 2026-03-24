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
* **Padrões:** MediatR, FluentValidation
* **Documentação:** Swagger
* **Orquestração:** Docker & Kubernetes
* **Observabilidade:** ILogger e tabela de "Auditoria"
* **Azure:**
  * `Serviço de Aplicativo (App Service)`: Utilizado para hospedar as APIs principais (api-catalog, api-notifications, api-payments, api-users). São ambientes gerenciados para rodar aplicações web e APIs.
  * `Plano do Serviço de Aplicativo (App Service Plan)`: Representa os recursos de hardware (CPU e Memória) onde os App Services rodam.
  * `Aplicativo de Funções (Azure Functions)`: O recurso notification-function indica o uso de computação serverless (sem servidor), ideal para tarefas disparadas por eventos ou execuções em segundo plano.
  * `Banco de Dados SQL (Azure SQL Database)`: Existem instâncias específicas para os domínios de catálogo, pagamentos e usuários (db_fcg_catalog, etc.).
  * `Servidor SQL (Azure SQL Server)`: O recurso fiap-srv-banco é o servidor lógico que gerencia e hospeda os bancos de dados SQL individuais mencionados acima.
  * `Serviço de Gerenciamento de API (API Management - APIM)`: O fcg-api-apim atua como um gateway, centralizando a exposição, segurança e o roteamento das suas APIs para o mundo externo.
  * `Namespace do Barramento de Serviço (Service Bus)`: O fiap-clound-game indica o uso de filas ou tópicos para comunicação assíncrona entre os seus serviços, garantindo desacoplamento.
  * `Application Insights`: O recurso fcg-logs é usado para monitorar o desempenho das aplicações, capturar exceções e analisar o comportamento dos usuários em tempo real.
  * `Workspace do Log Analytics`: Onde os dados de monitoramento e logs são agregados e consultados via KQL (Kusto Query Language).
  * `Grupo de Ações (Action Group)`: O Application Insights Smart Detection é configurado para disparar notificações (e-mail, SMS ou webhooks) caso anomalias sejam detectadas no sistema.

## 4. Configuração do Ambiente
Para que a aplicação funcione corretamente, edite o arquivo `appsettings.Development.json` seguindo o modelo abaixo:

```json
{
  "ConnectionStrings": {
    "ConnectionStrings": "{CONEXAO_BANCO}"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "ApplicationInsights": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    }
  },
  "Jwt": {
    "Key": "ChaveSuperSecretaComMaisDe32CaracteresAqui12345",
    "Issuer": "FCG-Users"
  },
  "ServiceBus": {
    "ConnectionString": "{CONEXAO_MENSAGERIA}"
  },
  "ApplicationInsights": {
    "ConnectionString": "{CONEXAO_LOG}"
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
