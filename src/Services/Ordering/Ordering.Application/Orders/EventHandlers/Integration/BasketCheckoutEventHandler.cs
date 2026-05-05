using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.Message.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(
                message.FirstName, message.LastName,
                message.EmailAddress,
                message.AddressLine,
                message.Country,
                message.State,
                message.ZipCode
            );

            var paymentDto = new PaymentDto(
                message.CardName,
                message.CardNumber,
                message.Expiration,
                message.CVV,
                message.PaymentMethod
            );

            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: OrderStatus.Pending,
                OrderItems: [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDto(orderId, new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), 1, 400),
                ]
            );
            return new CreateOrderCommand(orderDto);
        }
    }
}
