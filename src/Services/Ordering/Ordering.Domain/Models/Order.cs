namespace Ordering.Domain.Models
{
    /// <summary>
    /// Questa classe, che rappresenta la nostra entità "Order", funge da root aggregate, ovvero da base
    /// Alla quale poi, avremo entità (OrderItem), ValueObject (Address) --> oggetti più complessi, che però non hanno un'identità propria, ma sono parte dell'aggregato Order
    /// OrderPlacedEvent --> il nostro evento di dominio, che rappresenta un evento che si verifica quando un ordine viene effettuato. Questo evento può essere utilizzato 
    /// per notificare altri componenti del sistema o per eseguire azioni specifiche in risposta all'evento.
    /// </summary>
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        private IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        /*
         * Aggiungo i metodi all'interno del dominio, in quanto corrisponde all'architettura DDD, 
         * in cui le logiche di business sono incapsulate all'interno del dominio stesso, 
         * e non vengono esposte all'esterno. 
         * In questo modo, si garantisce che tutte le operazioni sull'aggregato Order siano coerenti e rispettino 
         * le regole di business definite.
        */
        public void AddOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(OrderItemId orderItemId)
        {
            var item = _orderItems.FirstOrDefault(x => x.Id == orderItemId);
            if (item != null) _orderItems.Remove(item);
        }

    }
}
