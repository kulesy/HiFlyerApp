using Blazored.LocalStorage;
using HiFlyerClassLibrary.Endpoints;
using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.Authentication;
using HiFlyerWebApp.Caches;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace HiFlyerWebApp.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IShopifyEndpoint _shopifyEndpoint;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ITokenCache _tokenCache;

        public AuthenticationService(IShopifyEndpoint shopifyEndpoint,
                                     AuthenticationStateProvider authStateProvider,
                                     ITokenCache tokenCache)
        {
            _shopifyEndpoint = shopifyEndpoint;
            _authStateProvider = authStateProvider;
            _tokenCache = tokenCache;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await _shopifyEndpoint.CreateCustomerAccessToken(loginModel);
            if (result.CustomerAccessTokenCreate.CustomerAccessToken == null)
                return null;
            var token = result.CustomerAccessTokenCreate.CustomerAccessToken;
            await _tokenCache.CreateCacheAsync(token);
            await ((AuthStateProvider)_authStateProvider).NotifyUserAuthenticationAsync(token.AccessToken);
            return token.AccessToken;
        }

        public async Task Logout()
        {
            await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}
