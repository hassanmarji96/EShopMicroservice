using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shopping cart cannot be null.");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("User name is required.");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.ShoppingCart, cancellationToken);
            await basketRepository.StoreBasketAsync(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            foreach (var item in shoppingCart.Items)
            {
                var discount = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= discount.Amount;
            }
        }
    }
}
