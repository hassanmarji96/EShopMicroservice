namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IOrderingDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

            if (order is null)
                throw new OrderNotFoundException(command.Order.Id);

            UpdateOrderWithNewValues(order, command.Order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updateShippingAddressDto = orderDto.ShippingAddress;
            var updateBillingAddressDto = orderDto.BillingAddress;
            var updatePaymentDto = orderDto.Payment;

            var updatedShippingAddress = Address.Of(updateShippingAddressDto.FirstName, updateShippingAddressDto.LastName, updateShippingAddressDto.EmailAddress, updateShippingAddressDto.AddressLine, updateShippingAddressDto.Country, updateShippingAddressDto.State, updateShippingAddressDto.ZipCode);
            var updatedBillingAddress = Address.Of(updateBillingAddressDto.FirstName, updateBillingAddressDto.LastName, updateBillingAddressDto.EmailAddress, updateBillingAddressDto.AddressLine, updateBillingAddressDto.Country, updateBillingAddressDto.State, updateBillingAddressDto.ZipCode);
            var updatedPayment = Payment.Of(updatePaymentDto.CardNumber, updatePaymentDto.CardName, updatePaymentDto.Expiration, updatePaymentDto.Cvv, updatePaymentDto.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: orderDto.Status
            );

            foreach (var orderItemDto in orderDto.OrderItems)
                order.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
        }
    }
}
