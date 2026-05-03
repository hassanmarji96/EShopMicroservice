# Asynchronous Communication
Nella comunicazione asincrona i messaggi vengono inviati dal client, senza aspettare una risposta immediata dal server. 
Il client può continuare a eseguire altre operazioni mentre il server elabora la richiesta e invia una risposta in un secondo momento.

Il protocollo più popolare per la comunicazione asincrona è AMPQ (Advanced Message Queuing Protocol), che è un protocollo di messaggistica 
open source che consente la comunicazione asincrona tra applicazioni.
Utilizzando AMPQ, il client può inviare messaggi utilizzando un message broker (Kafka/RabbitMQ) i quali gestiscono una coda di messaggi.
Il *producer* solitamente NON attende una risposta immediata. Il messaggio viene poi letto da un *consumer*/*subscriber* in modo asincrono.

Se vi sono delle interazioni pesanti tra servizi multipli, allora l'utilizzo di un sistema di messaggistica asincrona può essere più efficiente rispetto 
alla comunicazione sincrona, poiché consente ai servizi di lavorare in modo indipendente e di scalare in modo più efficiente.

# Fan-out - Publish/Subscribe pattern
- Il fan-out è un pattern di comunicazione. In questo pattern, un messaggio viene inviato a più destinatari contemporaneamente.
- Ogni destinatario riceve una copia del messaggio, potendo lavorare e processare il tutto in modo indipendente e parallelamente.
- Il modello publish/subscriber è un esempio di fan-out, in cui un publisher invia messaggi a più subscriber che si sono iscritti a ricevere quei messaggi.
- Il publisher non ha bisogno di conoscere chi sono i subscriber, stesso discorso per i subscriber che non devono conoscere chi è il publisher.

# Event-Driven Microservices Architecture (EDA)
- Event-Driven Microservices Architecture (EDA) è un'architettura software in cui i microservizi comunicano tra loro attraverso eventi.
- Quando un servizio vuole comunicare con un altro servizio, pubblica un evento in una coda di messaggio o in un event-bus. Gli altri servizi che sono interessati all'evento eseguono la sottoscrizione, prendendo l'evento che desiderano.
- La comunicazione asincrona consente ai sevizi di lavorare in modo indipendente, migliorando la scalabilità e la resilienza dell'architettura.
- Comunicazione decoupled: i servizi non devono conoscere l'esistenza degli altri servizi, ma solo gli eventi che pubblicano o sottoscrivono.

# RabbitMQ
- RabbitMQ è un message broker che implementa il protocollo AMPQ (Advanceed Message Queuing Protocol).
- Permette alle applicazione di comunica tra di loro, inviando e ricevendo messaggi, attraverso code di messaggi.
- Permette la comunicazione asincrona.
- I componenti principali di RabbitMQ sono:
	- Producer: è l'entità che invia i messaggi a RabbitMQ.
	- Queue: è una coda di messaggi in cui i messaggi vengono memorizzati fino a quando non vengono consumati dai consumer.
	- Consumer: è l'entità che riceve i messaggi da RabbitMQ e li elabora.
	- Exchange: è un componente che riceve i messaggi dai producer e li instrada alle code in base a determinate regole di routing.
	- Binding: è una regola che collega un exchange a una coda, specificando come i messaggi devono essere instradati.
	- FIFO: è un acronimo che sta per "First In, First Out", ovvero "primo arrivato, primo servito". In una coda FIFO, i messaggi vengono elaborati nell'ordine in cui sono stati ricevuti.


## RabbitMQ Exchange Types
RabbitMQ supporta diversi tipi di exchange, ognuno con un comportamento di routing specifico:
- Direct Exchange: utilizza una singola coda di routing per instradare i messaggi. I messaggi vengono inviati a una coda specifica in base a una chiave di routing esatta.
- Topic Exchange: i messaggi sono inviati a code differenti, sulla base in un **subject**. I messaggi in arrivo sono classificati ed inviati alla coda rispettiva.
- Fanout Exchange: i messaggi vengono inviati a tutte le code collegate all'exchange, indipendentemente dalla chiave di routing. Questo è utile per il pattern publish/subscribe.
- Headers Exchange: i messaggi vengono instradati in base a un set di intestazioni (headers) invece che a una chiave di routing. I messaggi vengono inviati a code che corrispondono alle intestazioni specificate.