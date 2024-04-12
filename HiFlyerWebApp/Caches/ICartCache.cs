using HiFlyerClassLibrary.Models.ShopifyModels;
using HiFlyerClassLibrary.Models;

namespace HiFlyerWebApp.Caches
{
    public interface ICartCache
    {
        Task AddProductToCacheAsync(CartProduct product);
        void AddStateChangeListeners(Action listener);
        void BroadcastStateChange();
        Task CreateCacheAsync(List<CartProduct> faces);
        Task DeleteCacheAsync();
        Task DeleteProductFromCacheAsync(CartProduct product);
        Task<List<CartProduct>> GetCachedCartAsync();
        Task<DateTime?> GetCachedDateAsync();
        void RemoveStateChangeListeners(Action listener);
    }
}