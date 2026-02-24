
namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByCategoryQuery for category: {Category}", query.Category);
            var products = await session.Query<Product>()
                .Where(p => p.Categories.Contains(query.Category))
                .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }
}
