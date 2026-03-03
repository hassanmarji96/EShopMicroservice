# STRUTTURA SOTTOSTANTE SERVIZIO BASKET.API
Il servizio Basket.API possiede due sorgenti dati:
1. Marten Document Database: Marten come menzionato per il servizio catalog è una libreria che trasforma
	PostgreSQL in un Database Documentale basato su transazioni .NET usando la funzionalità delle colonne JSON
	di PostgreSQL.
2. Redis Distributed Cache: Redis invece è uno store "in-memory" molto potente il quale risulta essere utile ed efficace per l'architettura a microservizi.

# Perchè Redis?
Redis è uno store chiave-valore conosciuto per le sue alte performance. Usato solitamente come servizio di caching, session-storage e molto altro.
Essendo "in-memory" risulta essere molto veloce nell'accedere ai dati. Supporta l'utilizzo di varie strutture dati rendendolo versatile per molti casi d'uso.

## Cache-Aside Pattern
Pattern utilizzato nei microservizi.
1. Quando il client ha bisogno di fetchare i dati, prima controlla che siano all'interno della cache.
2. Se i dati sono presenti nella cache, allora il client li fetcha e li restituisce al chiamante.
3. Se i dati NON sono presenti all'interno della cache, allora il client fetcha i dati direttamente dal DB, li salva nella cache e li restituisce al chiamante.

Il motivo per cui utilizzo una cache distrbuita è che mi serve che venga condivisa tra servizi multipli.

## Proxy Pattern
Il Pattern Proxy è un design pattern strutturale che fornisce un placeholder per un altro oggetto per controllare l'accesso ad esso. Questo pattern
crea un oggetto proxy che funge da intermediario per richieste intese al vero oggetto, consentendo di eseguire operazioni prima o dopo la richiesta 
al vero oggetto senza modificare il codice del vero oggetto.

## Decorator Pattern
Questo pattern è un design pattern strutturale che consente di aggiungere comportamenti a oggetti dinamicamente, senza alterarne la struttura.
Include un insieme di classi decoratori che vengono usate per estendere la funzionalità della classe originale senza modificarne il codice.
Per esempio può essere utile per aggiungere funzionalità di logging, caching o validazione a un servizio senza dover modificare il codice del servizio stesso.
Oppure per aggiungere funzionalità ad un oggetto a runtime.

### Implementazione
- Creare un decoratore astratto che implementa la stessa interfaccia del servizio originale. Successivamente il decoratore concreto aggiunge comportamenti aggiuntivi.
- ESEMPIO: Avendo L'interfaccia IBasketRepository, ed avendo una classe BasketRepository che implementa questa interfaccia, posso creare un decoratore
astratto CachedBasketRepository che implementa IBasketRepository e contiene un riferimento a un'istanza di IBasketRepository. 

Per utilizzare questo pattern utilizzo la **SCRUTOR LIBRARY** che è una libreria di estensione per il Dependency Injection in .NET, 
che semplifica la registrazione dei servizi e supporta la registrazione di decoratori in modo semplice e intuitivo.