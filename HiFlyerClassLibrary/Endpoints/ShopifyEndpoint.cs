using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.ShopifyModels;
using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using HiFlyerClassLibrary.Models.Authentication;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using AutoMapper;

namespace HiFlyerClassLibrary.Endpoints
{
    public class ShopifyEndpoint : IShopifyEndpoint
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;

        public ShopifyEndpoint(HttpClient httpClient,
                               IConfiguration config,
                               ILocalStorageService localStorage,
                               IMapper mapper)
        {
            _httpClient = httpClient;
            _config = config;
            _localStorage = localStorage;
            _mapper = mapper;
        }
        public async Task<GetProductsResponse> GetProducts()
        {
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:GetProducts"];
            var apiResult = await _httpClient.GetAsync(apiConnection);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = GetProductsResponse.FromJson(apiContent);
                return resultContent;
            }
            return null;
        }
        public async Task<GetProductByHandleResponse> GetProductByHandle(string handle)
        {
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:GetProductByHandle"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, handle);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = GetProductByHandleResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }

        public async Task<GetCustomerResponse> GetCustomer(string token)
        {
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:GetCustomer"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, token);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = GetCustomerResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }

        public async Task<CreateCustomerResponse> CreateCustomer(RegisterModel newCustomer)
        {
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CreateCustomer"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, newCustomer);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CreateCustomerResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }

        public async Task<CreateCustomerAddressResponse> CreateCustomerAddress(Customer customer, string customerAccessToken)
        {
            CreateCustomerAddressInput createCustomerAddressInput = new();
            MailingAddressInput addressInput = new();
            _mapper.Map(customer.DefaultAddress, addressInput);
            createCustomerAddressInput.MailingAddress = addressInput;
            createCustomerAddressInput.CustomerAccessToken = customerAccessToken;

            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CreateCustomerAddress"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, createCustomerAddressInput);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CreateCustomerAddressResponse.FromJson(apiContent);
                return resultContent;
            }
            return null;
        }

        public async Task<UpdateCustomerResponse> UpdateCustomer(Customer customer, string customerAccessToken)
        {
            UpdateCustomerInput updateCustomerAddressInput = new();
            CustomerUpdateInput customerInput = new();
            _mapper.Map(customer, customerInput);
            updateCustomerAddressInput.Customer = customerInput;
            updateCustomerAddressInput.CustomerAccessToken = customerAccessToken;

            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:UpdateCustomer"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, updateCustomerAddressInput);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = UpdateCustomerResponse.FromJson(apiContent);
                return resultContent;
            }
            return null;
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(Customer customer, string customerAccessToken)
        {
            UpdateCustomerAddressInput updateCustomerAddressInput = new();
            MailingAddressInput mailingAddress = new();
            _mapper.Map(customer.DefaultAddress, mailingAddress);
            updateCustomerAddressInput.MailingAddress = mailingAddress;
            updateCustomerAddressInput.CustomerAccessToken = customerAccessToken;
            updateCustomerAddressInput.Id = customer.DefaultAddress.Id;
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:UpdateCustomerAddress"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, updateCustomerAddressInput);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = UpdateCustomerAddressResponse.FromJson(apiContent);
                return resultContent;
            }
            return null;
        }

        public async Task<CreateCustomerAccessTokenResponse> CreateCustomerAccessToken(LoginModel existingCustomer)
        {
            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CreateCustomerAccessToken"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, existingCustomer);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CreateCustomerAccessTokenResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }

        public async Task<CreateCheckoutResponse> CreateCheckoutLoggedIn(List<CartProduct> cart, Customer customer)
        {
            List<CheckoutLineItemInput> checkoutLineInput = new();
            MailingAddressInput mailingAddressInput = new();
            foreach(var product in cart)
            {
                CheckoutLineItemInput checkoutLineItemInput = new();
                checkoutLineItemInput.VariantId = product.VariantId;
                checkoutLineItemInput.Quantity = product.Quantity;
                checkoutLineInput.Add(checkoutLineItemInput);
            }
            if (customer.Email is not null)
            {
                mailingAddressInput.Address1 = customer.DefaultAddress.Address1;
                mailingAddressInput.Address2 = customer.DefaultAddress.Address2;
                mailingAddressInput.City = customer.DefaultAddress.City;
                mailingAddressInput.Company = customer.DefaultAddress.Company;
                mailingAddressInput.Country = customer.DefaultAddress.Country;
                mailingAddressInput.FirstName = customer.DefaultAddress.FirstName;
                mailingAddressInput.LastName = customer.DefaultAddress.LastName;
                mailingAddressInput.Phone = customer.DefaultAddress.Phone;
                mailingAddressInput.Province = customer.DefaultAddress.Province;
                mailingAddressInput.Zip = customer.DefaultAddress.Zip;
                
            }

            CreateCheckoutLoggedInInput createCheckoutLoggedInInput = new()
            {
                Email = customer.Email,
                LineItems = checkoutLineInput,
                ShippingAddress = mailingAddressInput
            };

            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CreateCheckoutLoggedIn"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, createCheckoutLoggedInInput);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CreateCheckoutResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }
        public async Task<CreateCheckoutResponse> CreateCheckoutLoggedOut(List<CartProduct> cart)
        {
            List<CheckoutLineItemInput> checkoutLineInput = new();
            foreach (var product in cart)
            {
                CheckoutLineItemInput checkoutLineItemInput = new();
                checkoutLineItemInput.VariantId = product.VariantId;
                checkoutLineItemInput.Quantity = product.Quantity;
                checkoutLineInput.Add(checkoutLineItemInput);
            }

            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CreateCheckoutLoggedOut"];
            var apiResult = await _httpClient.PostAsJsonAsync(apiConnection, checkoutLineInput);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CreateCheckoutResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }


        public async Task<CheckoutCustomerAssociateResponse> CheckoutCustomerAssociate(string checkoutId, string customerAccessToken)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("checkoutId", checkoutId),
                new KeyValuePair<string, string>("customerAccessToken", customerAccessToken)
            });

            var apiConnection = _config["ApiConnectionString"] + _config["Shopify:CheckoutCustomerAssociate"];
            var apiResult = await _httpClient.PostAsync(apiConnection, data);
            if (apiResult.IsSuccessStatusCode)
            {
                var apiContent = await apiResult.Content.ReadAsStringAsync();
                var resultContent = CheckoutCustomerAssociateResponse.FromJson(apiContent);
                return resultContent;

            }
            return null;
        }
    }
}
