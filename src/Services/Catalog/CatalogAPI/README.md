# 1. Application Architecture
- ** Vertical Slice Architecture **: Ho scelto di organizzare il progetto seguendo l'architettura a fette verticali, che consente di suddividere l'applicazione in moduli indipendenti, ognuno responsabile di una specifica funzionalità.
L'applicazione viene divisa in "fette" verticali, basate sulle funzionalità (feature). Ogni fetta è indipendente, in modo tale da ridurre le dipendenze tra moduli dell'applicazione e promuove l'uso di team cross-funzionali.

# 2. Design Patterns
- **CQRS (Command Query Responsibility Segregation)**: Ho implementato il pattern CQRS per separare le operazioni di lettura e scrittura, migliorando la scalabilità e la manutenibilità dell'applicazione.
- **Mediator Pattern**: Ho utilizzato il pattern Mediator per facilitare la comunicazione tra i componenti dell'applicazione, riducendo le dipendenze dirette e migliorando la modularità del codice.
 Il pattern Mediator è utile per applicazioni complesse o di livello enterprise, dove il processamento delle richieste solitamente richiede più della semplice business logic.
 Il pacchetto MediatR fornisce una pipeline di "mediazione" dove cross-cutting concerns possono essere inseriti facilmente. 
# 3. Pacchetti utilizzati
- **MediatR**: Per facilitare l'implementazione del pattern CQRS
- **Carter for API Endpoints**: Per semplificare la definizione degli endpoint API e migliorare la leggibilità del codice.
- **Marten**: Utilizza PostgreSQL come se fosse un DB documentale.
- **FluentValidation**: Per implementare la validazione dei dati in modo fluida e intuitivo.
