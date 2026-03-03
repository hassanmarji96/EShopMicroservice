
namespace Basket.API.Repository
{
    /// <summary>
    /// In questa classe implemento sia il proxy pattern che il pattern decorator.
    /// Il proxy pattern la classe CachedBasketRepository funge da proxy per accedere alla repository reale, inoltrando le chiamate alla repository
    /// sottostante.
    /// Successivamente estenderemo le funzionalità della classe CachedBasketRepository implementando il pattern decorator.
    /// </summary>
    /// <param name="basketRepository"></param>
    public class CachedBasketRepository(IBasketRepository basketRepository) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await basketRepository.GetBasketAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            return await basketRepository.StoreBasketAsync(basket, cancellationToken);
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await basketRepository.DeleteBasketAsync(userName, cancellationToken);
        }
    }
}
