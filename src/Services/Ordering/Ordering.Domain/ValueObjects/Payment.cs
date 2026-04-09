namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; } = default!;
        public string? CardName { get; } = default!;
        public string ExpirationDate { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        /// <summary>
        /// Serve esclusivamente a Entity Framework Core per poter materializzare l'oggetto 
        /// dal database tramite reflection, senza esporre il costruttore pubblicamente.
        /// </summary>
        protected Payment() { }

        /// <summary>
        /// Creo il costruttore privato in modo tale da poter controllare la creazione dell'oggetto Payment. 
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardName"></param>
        /// <param name="expirationDate"></param>
        /// <param name="cvv"></param>
        /// <param name="paymentMethod"></param>
        private Payment(string cardNumber, string? cardName, string expirationDate, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            ExpirationDate = expirationDate;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        /// <summary>
        /// Il metodo Of è un factory method che consente di creare un'istanza di Payment in modo controllato,
        /// garantendo che tutti i parametri siano validi e, se necessario, applicando eventuali regole di business o validazioni prima di creare l'oggetto.
        /// In sintesi posso andare a controllare la logica di creazione dell'oggetto, senza interferire ed esporre il costruttore pubblico, 
        /// mantenendo così l'integrità dell'oggetto e garantendo che venga creato in uno stato valido.
        /// </summary>
        public static Payment Of(string cardNumber, string? cardName, string expirationDate, 
            string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expirationDate);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

            return new Payment(cardNumber, cardName, expirationDate, cvv, paymentMethod);
        }
    }
}
