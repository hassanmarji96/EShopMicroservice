# 1. Application Architecture
## 1.1. DDD, CQRS and Clean Architecture
- La clean architecture in questo caso è suddivisa in 4 livelli con una class library di riferimento:
- Ovvero il progetto principale è quello dell'API, che fa riferimento a 3 class library:
  - **Domain**: contiene la logica di business e le entità.
  - **Application**: gestisce i casi d'uso e le interazioni tra le entità.
  - **Infrastructure**: si occupa dell'accesso ai dati e delle dipendenze esterne.

# 2. Design Patterns & Principles
## 2.1. SOLID
Il pattern SOLID è un pattern che aiuta a scrivere codice più pulito, mantenibile e scalabile. I principi SOLID sono:
- **S**: Single Responsibility (ogni componente o modulo deve essere responsabile di una sola cosa)
- **O**: Open/Closed (nel design di sistemami, i componenti dovrebbero essere aperti per l'estensione ma chiusi per la modifica)
- **L**: Liskov Substitution (le classi derivate devono essere sostituibili con le classi base senza alterare il comportamento del programma)
- **I**: Interface Segregation (i client non devono essere costretti a dipendere da interfacce che non utilizzano)
- **D**: Dependency Inversion (le dipendenze devono essere astratte e non concrete)

## 2.2. Separation of Concerns (SoC)
Consiste nel separare il programma in sezioni distinte, ognuna con una responsabilità specifica. Limita l'allocamento di responsabilità a un singolo modulo o componente.

## 2.3. Domain-Driven Design (DDD)
DDD ci aiuta a risolvere problemi complessi, utilizzando il motto "divide-et-impera". Ovvero, spezza il problema in tanti
piccoli problemi, ognuno con una soluzione più semplice.

# 3. Clean Architecture
La CC è un'architettura software che mira a separare i "compiti" di un'applicazione e creare sistemi che sono indipendenti.
## 3.1. Principi chiave
- Indipendenza dagli Framework: L'architettura non dipende da librerie o framework specifici.
- Testabilità: Il codice è facilmente testabile, con dipendenze chiaramente definite.
- UI Agnostic: La logica di business è separata dalla presentazione, permettendo di cambiare l'interfaccia utente senza influenzare la logica.
- Database Agnostic: La logica di business è indipendente dal database, consentendo di cambiare il sistema di persistenza senza modificare la logica.
- External System Agnostic: La logica di business è indipendente da sistemi esterni, facilitando l'integrazione con diversi servizi.

## 3.2. Struttura dei livelli
- Layer 1: Entities (Domain)
	- Rappresentano i concetti fondamentali del dominio e contengono la logica di business.
	- Esempi: Order, Product, Customer
- Layer 2: Use Cases (Application)
	- Definiscono i casi d'uso e le interazioni tra le entità.
	- Esempi: OrderItem, CancelOrder, CreateOrder
- Layer 3: Interface Adapters (Infrastructure)
	- Si occupano di adattare le interfacce tra i livelli, come i controller API, i repository e i servizi esterni.
	- Esempi: Mapping dei dati dal database models alle entità del dominio, controller API che gestiscono le richieste HTTP
- Layer 4: Frameworks and Drivers (Infrastructure/External Concerns)
	- Si occupano delle dipendenze esterne e dei framework utilizzati nell'applicazione.
	- Esempi: REST Controllers, database repos
