namespace Basket.API.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDocumentSession _session;
    public BasketRepository(IDocumentSession session)
    {
        _session = session;
    }
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);

        if (basket is null)
            throw new BasketNotFoundException(userName);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        _session.Store(basket);
        await _session.SaveChangesAsync();
        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);

        if (basket is null)
            throw new BasketNotFoundException(userName);

        _session.Delete(basket);
        await _session.SaveChangesAsync(cancellationToken);
        return true;
    }
}
