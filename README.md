# Projeto Locadora

## Sobre <a name = "about"></a>

Projeto desenvolvido como teste técnico para E-Auditoria.

### Pre Requisitos

1. Visual Studio Code
2. Docker

## Cuidado <a name = "usage"></a>

<h1>Se for usar o Visual Studio para debugar o projeto:</h1>

1. Comente o todo serviço 'app_backend' em '/backend/docker-compose.yml'
2. Mude a linha 34 de '/backend/Program.cs' para
```
    var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
```

## Configurando e Iniciando Projeto <a name = "getting_started"></a>

1. Entre na pasta backend.
2. Execute o docker compose
```
docker compose up --build
```

3. Acesse a api em http://localhost:5001/swagger/index.html

4. Entre na pasta frontend.

5. Execute o docker compose
```
docker compose up --build
```

6. Acesse o projeto frontend em http://localhost:3000

<h1>Agora Insira os dados no banco de dados e verifique o projeto :)</h1>