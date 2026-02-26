# STRUTTURA SOTTOSTANTE SERVIZIO BASKET.API
Il servizio Basket.API possiede due sorgenti dati:
1. Marten Document Database: Marten come menzionato per il servizio catalog è una libreria che trasforma
	PostgreSQL in un Database Documentale basato su transazioni .NET usando la funzionalità delle colonne JSON
	di PostgreSQL.
2. Redis Distributed Cache: Redis invece è uno store "in-memory" molto potente il quale risulta essere utile ed efficace per l'architettura a microservizi

