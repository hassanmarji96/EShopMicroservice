
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResponse>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must be provided");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be required").Length(2,150).WithMessage("Name must be between 2 and 150");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories must be required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile must be required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }

    internal class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateProductRequestCommand for product with name {Name}", command.Name);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(command.Id);

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
