namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, 
                List<string> Categories, 
                string Description, 
                string ImageFile, 
                decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be required");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories must be required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile must be required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }

    internal class CreateProductCommandHandler (IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Categories = command.Categories,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
