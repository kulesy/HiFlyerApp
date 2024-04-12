using HiFlyerClassLibrary.Models.ShopifyModels;

namespace HiFlyerWebApp.Caches
{
    public interface ITokenCache
    {
        Task CreateCacheAsync(CustomerAccessToken token);
        Task DeleteCacheAsync();
        Task<string> GetCachedTokenAsync();
    }
}