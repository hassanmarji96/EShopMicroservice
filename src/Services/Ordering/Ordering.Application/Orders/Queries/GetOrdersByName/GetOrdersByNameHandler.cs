namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IOrderingDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            // 1. ottengo gli ordini in base al nome con il dbcontext
            // 2. restituisci gli ordini
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
