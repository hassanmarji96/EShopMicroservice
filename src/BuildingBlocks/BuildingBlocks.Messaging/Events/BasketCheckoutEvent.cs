namespace BuildingBlocks.Messaging.Events
{
    /// <summary>
    /// In questa classe, utilizzo solamente tipi primitivi per garanire che l'evento sia facilmente (de)serializzabile e 
    /// che non ci siano problemi di compatibilità tra i servizi che lo consumano.
    /// </summary>
    public record BasketCheckoutEvent : IntegrationEvent
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;

        // Shipping and Billing Address
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;

        // Pagamento
        public string CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;
    }
}
