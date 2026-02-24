
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequestCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResponse>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductRequestCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(UpdateProductRequestCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateProductRequestCommand for product with name {Name}", command.Name);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException();

            var updatedProduct = new Product
            {
                Id = command.Id,
                Name = command.Name,
                Categories = command.Categories,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Update(updatedProduct);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResponse(true);
        }
    }
}
