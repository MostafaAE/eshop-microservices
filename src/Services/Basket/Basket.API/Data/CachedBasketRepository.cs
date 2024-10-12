using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(
    IBasketRepository basketRepository, IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasked = await cache.GetStringAsync(userName, cancellationToken);

        if(!string.IsNullOrEmpty(cachedBasked))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasked)!;

        var basket =  await basketRepository.GetBasket(userName, cancellationToken);

        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket));

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await basketRepository.StoreBasket(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await basketRepository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
