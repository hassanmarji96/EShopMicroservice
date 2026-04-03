using MediatR;

namespace Ordering.Infrastructure.Data.Interceptors
{
    /// <summary>
    /// Interceptor EF Core che si occupa di pubblicare i Domain Events degli aggregati
    /// prima che le modifiche vengano salvate nel database.
    /// Intercetta sia le operazioni sincrone che quelle asincrone
    /// </summary>
    /// <param name="mediator">Istanza di IMediator usata per pubblicare i Domain Events.</param>
    public class DispatchDomainInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        /// <summary>
        /// Intercetta l'operazione asincrona di salvataggio.
        /// Pubblica i Domain Events in modo asincrono prima di procedere con il salvataggio.
        /// </summary>
        /// <param name="eventData">Dati contestuali dell'evento EF Core, incluso il <see cref="DbContext"/> attivo.</param>
        /// <param name="result">Risultato di interception corrente, propagato alla base.</param>
        /// <param name="cancellationToken">Token per la cancellazione dell'operazione asincrona.</param>
        /// <returns>
        /// Un <see cref="ValueTask{TResult}"/> che rappresenta il risultato asincrono dell'interception
        /// da passare alla pipeline EF Core.
        /// </returns>
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Raccoglie tutti i Domain Events dagli aggregati tracciati dal <see cref="DbContext"/>,
        /// li svuota da ciascun aggregato e li pubblica tramite <see cref="IMediator"/>.
        /// </summary>
        /// <remarks>
        /// Il metodo segue questo flusso:
        /// <list type="number">
        ///     <item>Recupera gli aggregati con Domain Events pendenti dal <see cref="ChangeTracker"/>.</item>
        ///     <item>Raccoglie tutti i Domain Events in una lista.</item>
        ///     <item>Svuota i Domain Events da ogni aggregato prima della pubblicazione.</item>
        ///     <item>Pubblica ogni Domain Event tramite MediatR.</item>
        /// </list>
        /// </remarks>
        /// <param name="context">
        /// Il <see cref="DbContext"/> attivo. Se <see langword="null"/>, il metodo termina senza eseguire operazioni.
        /// </param>
        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var aggregates = context.ChangeTracker
                                .Entries<IAggregate>()
                                .Where(a => a.Entity.DomainEvents.Any())
                                .Select(a => a.Entity);

            var domainEvents = aggregates
                                .SelectMany(a => a.DomainEvents)
                                .ToList();

            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            foreach(var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
