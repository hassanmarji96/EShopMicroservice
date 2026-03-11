namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; } = default!;
        public string? CardName { get; } = default!;
        public string ExpirationDate { get; } = default!;
        public string Cvv { get; } = default!;
        public int PaymentMethod { get; } = default!;
    }
}
