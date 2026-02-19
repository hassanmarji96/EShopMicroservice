using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Rappresenta un comando che non restituisce un valore.
    /// </summary>
    public interface ICommand : IRequest<Unit>
    {

    }

    /// <summary>
    /// Rappresenta un comando che restituisce una risposta.
    /// </summary>
    /// <typeparam name="TResponse">Il tipo della risposta restituita dal comando.</typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
