# 🎮 FIAP Cloud Games - UsersAPI

Responsável pela comunicação com o usuário através do envio de e-mails (simulados via log) baseados em eventos do sistema.

## 1. Funcionalidades
* Envio de e-mail de boas-vindas para novos usuários.
* Envio de confirmação de compra de jogos.
* Na listagem de usuários e grupos, foi implementado o Redis para cache, visando a redução da latência no banco de dados.

## 2. Fluxo Orientado a Eventos
Este serviço é um consumidor puramente reativo.

* **Consumidos:**
    * `UserCreatedEvent`: Gatilho para o envio do e-mail de boas-vindas.
    * `PaymentProcessedEvent`: Se o status for `Approved`, dispara o e-mail de confirmação da compra com os detalhes do jogo.

## 3. Tecnologias
* **Linguagem:** .NET 10.
* **Banco de Dados:** SQL Server e MongoDB.
* **Padrões:** MediatR, FluentValidation.
* **Documentação:** Swagger.
* **Orquestração:** Docker, Docker Compose, Kubernetes (K8s).
* **Prometheus:** Observabilidade.
* **Logs:** ILogger.
* **MongoDB:** Utilizado para armazenar logs de auditoria de dados que apresentam flexibilidade.
* **Redis:** Otimização com Cache para reduzir latência e carga nos bancos de dados.
* **Elasticsearch:** Motor de busca e análise distribuído capaz de processar grandes volumes de dados em tempo quase real.
* **Azure DevOps:** CI/CD.
* **Terraform:** Desenvolvimento de Infrastructure as a Service IaaS.

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
  * `Azure Cache for Redis`: fcg-fiap-redis: Representa a instância específica ou o cluster do Azure Cache for Redis provisionado para o ambiente da Fiap (FCG). É onde os recursos de memória e a capacidade de taxa de transferência (throughput) são efetivamente alocados e consumidos pelas suas APIs.
  * `Azure DocumentDB (com compatibilidade com MongoDB)`: audit-db: Banco de dados específico (ou coleção) hospedado no Azure DocumentDB, dedicado exclusivamente ao armazenamento de logs, históricos de alterações e rastreabilidade de ações do sistema (auditoria).
  * `Balanceador de Carga (Load Balancer)`: Kubernetes: Representa o componente que distribui o tráfego de rede que chega entre várias instâncias de uma aplicação (como os diferentes App Services ou contêineres). Ele evita que um único servidor fique sobrecarregado e direciona os usuários apenas para os recursos que estão funcionando corretamente.
  * `Cofre de Chaves (Key Vault - kv-fcg-prod-253)`: Utilizado para armazenar e gerenciar de forma segura credenciais secretas, chaves criptográficas e certificados das APIs do ambiente de produção. É um serviço em nuvem que protege informações sensíveis (como strings de conexão de banco de dados e chaves de API), garantindo que elas não fiquem expostas diretamente no código-fonte das aplicações.
  * `Serviço Kubernetes do Azure (AKS)`: Conjunto de Escalas de Máquinas Virtuais (VMSS - aks-system-31960874-vmss): Representa a infraestrutura de hardware subjacente (os nós do cluster). É o grupo de máquinas virtuais idênticas e gerenciadas automaticamente que fornecem a CPU e a memória necessárias para rodar os pods do Kubernetes.
  * `Azure Container Registry (ACR / acrfcgprod)`: Utilizado para armazenar e gerenciar imagens de contêineres Docker privadas de forma segura. É o repositório centralizado onde as imagens das APIs (como api-catalog ou api-payments) são publicadas antes de serem implantadas nos ambientes de execução.
  * `Pool de Agentes do AKS (aks-agentpool)`: Representa o grupo de máquinas virtuais, Grupo de Segurança de Rede (NSG): Utilizado para filtrar e controlar o tráfego de rede que entra e sai dos recursos do Azure. Funciona como um firewall virtual que define regras de segurança baseadas em IPs e portas.
  * `Serviço do Kubernetes (AKS - Azure Kubernetes Service)`: Utilizado para hospedar e orquestrar os microsserviços principais (aks-fcg-prod) em contêineres. É um ambiente gerenciado que automatiza a implantação, o escalonamento e o gerenciamento das aplicações, garantindo alta disponibilidade e resiliência.
  
  <img width="1273" height="839" alt="image" src="https://github.com/GilDias987/FCG/blob/main/FCG/teste/Captura%20de%20tela%202026-05-19%20221240.png" />
  
  <br/>
  <br/>
  
  * `Azure DevOps organization (fiap-fcg-org)`: Representa a organização principal dentro do ecossistema, funcionando como o limite lógico superior (tenant) que agrupa todos os projetos, usuários e faturamentos da instituição.
  
  <img width="1273" height="839" alt="image" src="https://github.com/GilDias987/FCG/blob/main/FCG/teste/Captura%20de%20tela%202026-05-19%20221357.png" />

  <br/>
  <br/>  

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
    "ConnectionString": "{CONEXAO_SERVICE_BUS}"
  },
  "Redis": {
    "ConnectionString": "{CONEXAO_REDIS}"
  },
  "Mongodbsql": {
    "ConnectionString": "{CONEXAO_MONGODB}"
  },
  "ApplicationInsights": {
    "ConnectionString": "{CONEXAO_LOG}"
  },
  "AllowedHosts": "*"
}
```

## 👥 Integrantes
- **Fase 4 - Nome do Grupo:**: 12.
    - **Participantes:**: 
      - Alexandre Araújo da Silva (AlexandreAraujo).
      - Josegil Dias Frota Figueira (gildiasfrota).
      - Miguel de Oliveira Gonçalves (miguel084).
