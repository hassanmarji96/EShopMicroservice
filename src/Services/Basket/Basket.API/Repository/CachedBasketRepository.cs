using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repository
{
    /// <summary>
    /// In questa classe implemento sia il proxy pattern che il pattern decorator.
    /// Il proxy pattern la classe CachedBasketRepository funge da proxy per accedere alla repository reale, inoltrando le chiamate alla repository
    /// sottostante.
    /// Successivamente estenderemo le funzionalità della classe CachedBasketRepository implementando il pattern decorator.
    /// </summary>
    /// <param name="basketRepository"></param>
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cacheBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;

            var basket = await basketRepository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasketAsync(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasketAsync(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }
    }
}
