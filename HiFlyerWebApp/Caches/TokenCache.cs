using Blazored.LocalStorage;
using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.ShopifyModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiFlyerWebApp.Caches
{
    public class TokenCache : ITokenCache
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private string tokenName;

        public TokenCache(ILocalStorageService localStorage, IConfiguration config)
        {
            _localStorage = localStorage;
            _config = config;
            tokenName = _config["authTokenName"];
        }

        public async Task<string> GetCachedTokenAsync()
        {
            var cart = await _localStorage.GetItemAsync<CustomerAccessToken>(tokenName);
            if (cart == null)
                return null;
            else if (cart.ExpiresAt.UtcDateTime <= DateTime.UtcNow)
                return null;
            else if (cart.ExpiresAt.UtcDateTime > DateTime.UtcNow)
                return cart.AccessToken.Trim('"');
            else
                return null;
        }

        public async Task CreateCacheAsync(CustomerAccessToken token)
        {
            await _localStorage.SetItemAsync(tokenName, token);
        }

        public async Task DeleteCacheAsync()
        {
            await _localStorage.RemoveItemAsync(tokenName);
        }
    }
}
