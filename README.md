# shorturl

## Postgresql & PgAdmin

### Quick Start
* Entre no diretório:  `cd .\src\WebApi`
* Rode este comando: `docker-compose up -d`


### Environments
Este compose contém as seguintes variáveis:

* `POSTGRES_USER` o valor padrão é **postgres**
* `POSTGRES_PASSWORD` o valor padrão é **12345**
* `PGADMIN_PORT` o valor padrão é **5050**
* `PGADMIN_DEFAULT_EMAIL` o valor padrão é **pgadmin4@pgadmin.org**
* `PGADMIN_DEFAULT_PASSWORD` o valor padrão é **admin**

### Acesso ao Postgresql:
* `localhost:5432`
* **Username:** postgres (as a default)
* **Password:** 12345 (as a default)

### Acesso ao PgAdmin:
* **URL:** `http://localhost:5050`
* **Username:** pgadmin4@pgadmin.org (as a default)
* **Password:** admin (as a default)

### Adicionar novo servidor no PgAdmin:
* **Host name/address** `postgres`
* **Port** `5432`
* **Username** as `POSTGRES_USER`, by default: `postgres`
* **Password** as `POSTGRES_PASSWORD`, by default `12345`
