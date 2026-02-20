namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", 
            async (CreateProductRequest request, ISender sender) =>
            {
                // Mappo la richiesta dell'oggetto in arrivo, in modo che sia conforme al comando che mi aspetto di ricevere all'interno del mio handler,
                // in questo modo non devo preoccuparmi di eventuali differenze tra i due oggetti, e posso semplicemente adattarli l'uno all'altro.
                var command = request.Adapt<CreateProductCommand>();
                // Invio il comando al mio handler, che si occuperà di eseguire la logica di business necessaria per creare un nuovo prodotto.
                var result = await sender.Send(command);
                // Mappo il risultato ottenuto dall'handler in un oggetto di risposta, che sarà restituito al client che ha effettuato la richiesta.
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Create Product");
        }
    }
}
