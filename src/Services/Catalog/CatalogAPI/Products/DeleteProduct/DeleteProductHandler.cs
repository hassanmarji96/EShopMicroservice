

namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductCommand (Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete Product Handler");
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if(product is null)
            {
                logger.LogInformation("Product not found");
                throw new ProductNotFoundException(command.Id);
            }

            session.Delete<Product>(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }


    }
}
