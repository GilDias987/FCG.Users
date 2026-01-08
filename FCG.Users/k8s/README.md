# Kubernetes - UsersAPI

Este diretório contém os manifestos Kubernetes para executar o microsserviço `users-api`.

## Pré-requisitos

- Docker Desktop com Kubernetes habilitado **ou** outro cluster Kubernetes configurado
- `kubectl` instalado e apontando para o contexto correto (ex.: `docker-desktop`)
- Projeto já compilado e Docker funcional

## 1. Build da imagem Docker

No diretório raiz da solução (onde está o arquivo `docker-compose.yml`), execute:

```bash
docker build -t users-api:latest -f FCG.Users.WebAPI/Dockerfile .
```

Verifique se a imagem foi criada:

```bash
docker images users-api
```

Você deve ver `users-api` com tag `latest` na lista.

## 2. Aplicar os manifestos Kubernetes

Ainda no diretório raiz da solução (onde está a pasta `k8s`), execute:

```bash
kubectl apply -f k8s
```

Isso irá criar:

- `Deployment` `users-api-deployment`
- `Service` `users-api`
- `ConfigMap` `users-api-config`
- `Secret` `users-api-secret`

## 3. Verificar os recursos

Verifique se os pods e serviços estão em execução:

```bash
kubectl get pods
kubectl get svc
```

O pod do `users-api` deve estar com status `Running`.

## 4. Acessar a API

O `Service` está configurado como `NodePort` na porta `30080`. Para acessar a API:

```text
http://localhost:30080
```

Se o cluster não estiver rodando localmente (por exemplo, em outro nó), use o IP do nó em vez de `localhost`.

## 5. Atualizar a aplicação

Sempre que alterar o código da API:

1. Rebuild da imagem:
   ```bash
   docker build -t users-api:latest -f FCG.Users.WebAPI/Dockerfile .
   ```
2. Reiniciar o deployment para forçar uso da nova imagem:
   ```bash
   kubectl rollout restart deployment users-api-deployment
   ```
3. Acompanhar o status:
   ```bash
   kubectl get pods
   ```

## 6. Limpar recursos

Para remover todos os recursos criados pelos manifestos desta pasta:

```bash
kubectl delete -f k8s
```
