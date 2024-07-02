# ToDoList Application

## Overview

Esta aplicação é composta por um frontend em Angular e um backend em .NET Web API, utilizando Domain-Driven Design (DDD), clean code e clean architecture, e testes de unidade em cada camada para garantir a qualidade do código.

ATENÇÃO: o projeto foi criado no Visual Studio, e a visualização correta da estrutura de pastas/camadas só será possível caso o Visual Studio esteja sendo utilizado, conforme imagem abaixo.

## Estrutura do Projeto

- **frontend**: Contém o código do frontend em Angular.
  - `src`: Código fonte do Angular.
  - `Dockerfile`: Dockerfile para construir a imagem do frontend.

- **backend**: Contém o código do backend em .NET, dividido em 4 camadas:
  - `Api`: Código da API Web.
  - `Application`: Lógica de aplicação.
  - `Domain`: Entidades e lógica de domínio.
  - `Infrastructure`: Configurações e repositórios de infraestrutura.
  - Testes para cada camada da aplicação.
 
- **banco de dados**: Foi utilizado SQL Server para a persistência dos dados

- **docker-compose**: Arquivos de configuração do Docker Compose.
  - `docker-compose.yml`: Arquivo principal para orquestrar os serviços do Docker.

## Pré-requisitos

- Docker e Docker Compose instalados na máquina.

## Como Rodar a Aplicação com Docker Compose

1. **Clone o repositório**

   ```bash
   git clone https://github.com/GabrielWeissReis/ToDoList-Docker.git
   cd ToDoList-Docker
   ```

2. **Contrua e inicie os contêineres**

Execute o comando abaixo na raiz do projeto onde está localizado o arquivo docker-compose.yml:

   ```bash
   docker-compose up --build
   ```

3. **Acesse a aplicação**

**Frontend**: http://localhost:4200

**Backend**: http://localhost:8080/swagger/index.html

## TODO
- `Testes`: adicionar maior cobertura de testes.
- `API Gateway`: adicionar API Gateway, por exemplo Ocelot, para termos um ponto de entrada único de requisições para a API e eventuais futuros microsserviços de backend.
- `Docker Hub Container Image Library`: adicionar as imagens ao Docker Hub para agilizar o tempo de download.
  
## Imagens do Projeto

![image](https://github.com/GabrielWeissReis/ToDoList-Docker/assets/20742093/88b4fb70-c7bc-429d-8b47-991f156a7841)
![image](https://github.com/GabrielWeissReis/ToDoList-Docker/assets/20742093/14902322-1e4e-4067-9bf9-8f78c2fbbbcd)
![image](https://github.com/GabrielWeissReis/ToDoList-Docker/assets/20742093/732987a6-75a8-4057-9eed-adb9869bb3ac)
![image](https://github.com/GabrielWeissReis/ToDoList-Docker/assets/20742093/bf07596a-1b16-430c-b2ab-2d31155db901)
![image](https://github.com/GabrielWeissReis/ToDoList-Docker/assets/20742093/6601a122-683a-4be5-98b4-f81839457c3c)
