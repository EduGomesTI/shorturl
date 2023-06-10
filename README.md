# shorturl

## Postgresql & PgAdmin

### Quick Start
* Entre no diretório:  `cd .\src\WebApi`
* Rode este comando: `docker-compose up -d`

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

## Migrations

### Tabela ShortUrl
* Pelo Package Manager Console selecione Infrastructure como Default project
* Add-Migration InitialShortUrl -Context ShortUrlDbContext
* Update-Database -Context ShortUrlDbContext

### Tabela Outbox
* Pelo Package Manager Console selecione Infrastructure como Default project
* Add-Migration InitialOutBox -Context OutboxDbContext
* Update-Database -Context OutboxDbContext