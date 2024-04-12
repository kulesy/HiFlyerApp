using Blazored.LocalStorage;
using HiFlyerClassLibrary.Enum;
using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.ShopifyModels;
using HiFlyerWebApp.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiFlyerWebApp.Caches
{
    public class CartCache : ICartCache
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ErrorListStore _errorListStore;
        private string cartName = "cart";
        private string cartCacheDate = "cacheDate";

        public CartCache(ILocalStorageService localStorage, ErrorListStore errorListStore)
        {
            _localStorage = localStorage;
            _errorListStore = errorListStore;
        }

        public async Task<List<CartProduct>> GetCachedCartAsync()
        {
            var cart = await _localStorage.GetItemAsync<List<CartProduct>>(cartName);
            if (cart == null)
                return null;
            return cart;
        }

        public async Task<DateTime?> GetCachedDateAsync()
        {
            return await _localStorage.GetItemAsync<DateTime?>(cartCacheDate);
        }
        public async Task CreateCacheAsync(List<CartProduct> cart)
        {
            await _localStorage.SetItemAsync(cartName, cart);
            await _localStorage.SetItemAsync(cartCacheDate, DateTime.UtcNow);
        }

        public async Task AddProductToCacheAsync(CartProduct product)
        {
            var cart = await GetCachedCartAsync();
            if (cart == null)
                cart = new();
            var matchedProduct = cart.Find(e => (e.VariantTitle == product.VariantTitle && e.Title == product.Title));
            await DeleteCacheAsync();

            if (matchedProduct != null && (matchedProduct.Quantity + product.Quantity) <= product.MaxQuantity)
                matchedProduct.Quantity += product.Quantity;
            else if (matchedProduct != null && (matchedProduct.Quantity + product.Quantity) > product.MaxQuantity)
                _errorListStore.AddError(new() { Message = "Stock limit reached.", State = ErrorState.Warning });
            else if (matchedProduct == null)
                cart.Add(product);

            await _localStorage.SetItemAsync(cartName, cart);
            await _localStorage.SetItemAsync(cartCacheDate, DateTime.UtcNow);
            BroadcastStateChange();
        }

        public async Task DeleteProductFromCacheAsync(CartProduct product)
        {
            var cart = await GetCachedCartAsync();
            var matchedProduct = cart.Find(e => (e.VariantTitle == product.VariantTitle && e.Title == product.Title));
            cart.Remove(matchedProduct);
            await CreateCacheAsync(cart);
            BroadcastStateChange();
        }

        public async Task DeleteCacheAsync()
        {
            await _localStorage.RemoveItemAsync(cartName);
            await _localStorage.RemoveItemAsync(cartCacheDate);
        }

        ////////////////////////

        private Action _listeners;

        public void AddStateChangeListeners(Action listener)
        {
            _listeners += listener;
        }

        public void RemoveStateChangeListeners(Action listener)
        {
            _listeners -= listener;
        }

        public void BroadcastStateChange()
        {
            _listeners.Invoke();
        }
    }
}
