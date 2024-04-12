using Blazored.LocalStorage;
using HiFlyerClassLibrary.Endpoints;
using HiFlyerWebApp.Caches;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiFlyerWebApp.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IShopifyEndpoint _shopifyEndpoint;
        private readonly ITokenCache _tokenCache;
        private readonly AuthenticationState _anonymous;
        private readonly string _authTokenName;

        public AuthStateProvider(
            HttpClient httpClient,
            IShopifyEndpoint shopifyEndpoint,
            ITokenCache tokenCache
            )
        {
            _httpClient = httpClient;
            _shopifyEndpoint = shopifyEndpoint;
            _tokenCache = tokenCache;
            _anonymous = new AuthenticationState(
                   new ClaimsPrincipal(
                   new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenCache.GetCachedTokenAsync();
            
            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }

            var authResult = await NotifyUserAuthenticationAsync(token);

            if (authResult == false)
            {
                return _anonymous;
            }
            return new AuthenticationState(
                   new ClaimsPrincipal(
                   new ClaimsIdentity(
                   new List<Claim>
                   {
                        new Claim(ClaimTypes.Role, "Customer")
                   }, "jwtAuthType")));
        }

        public async Task<bool> NotifyUserAuthenticationAsync(string token)
        {
            Task<AuthenticationState> authState;
            var result = await _shopifyEndpoint.GetCustomer(token);
            if (result.Customer is null)
            {
                await NotifyUserLogout();
                return false;
            }
            var authenticatedUser = new ClaimsPrincipal(
                                    new ClaimsIdentity(
                                    new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Role, "Customer") 
                                    }));
            authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            return true;

        }

        public async Task NotifyUserLogout()
        {
            await _tokenCache.DeleteCacheAsync();
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
