namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IOrderingDbContext dbContext) 
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // 1. Creo entità Order dall'oggetto command
            // 2. Aggiungo l'entità al DB
            // 3. restituisco il risultato

            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddressDto = orderDto.ShippingAddress;
            var billingAddressDto = orderDto.BillingAddress;
            var paymentDto = orderDto.Payment;
            var shippingAddress = Address.Of(shippingAddressDto.FirstName, shippingAddressDto.LastName, shippingAddressDto.EmailAddress, shippingAddressDto.AddressLine, shippingAddressDto.Country, shippingAddressDto.State, shippingAddressDto.ZipCode);
            var billingAddress = Address.Of(billingAddressDto.FirstName, billingAddressDto.LastName, billingAddressDto.EmailAddress, billingAddressDto.AddressLine, billingAddressDto.Country, billingAddressDto.State, billingAddressDto.ZipCode);
            
            var newOrder = Order.Create(
                orderId: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(paymentDto.CardNumber, paymentDto.CardName, paymentDto.Expiration, paymentDto.Cvv, paymentDto.PaymentMethod)
            );

            foreach (var orderItemDto in orderDto.OrderItems)
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);

            return newOrder;
        }
    }
}
