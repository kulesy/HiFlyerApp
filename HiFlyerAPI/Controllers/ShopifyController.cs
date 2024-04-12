using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.ShopifyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HiFlyerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopifyController : ControllerBase
    {
        private readonly IShopifyClient _shopifyClient;

        public ShopifyController(IShopifyClient shopifyClient)
        {
            _shopifyClient = shopifyClient;
        }

        [Route("GetProducts")]
        [HttpGet]
        public async Task<IGetProductsResult> GetProducts()
        {
            var result = await _shopifyClient.GetProducts.ExecuteAsync();
            return result.Data;
        }

        [Route("GetProductByHandle")]
        [HttpPost]
        public async Task<IGetProductByHandleResult> GetProductsByHandle([FromBody] string handle)
        {
            var result = await _shopifyClient.GetProductByHandle.ExecuteAsync(handle);
            return result.Data;
        }

        [Route("GetCustomer")]
        [HttpPost]
        public async Task<IGetCustomerResult> GetCustomer([FromBody] string token)
        {
            var result = await _shopifyClient.GetCustomer.ExecuteAsync(token);
            return result.Data;
        }

        [Route("CreateCustomer")]
        [HttpPost]
        public async Task<ICreateCustomerResult> CreateCustomer([FromBody] CustomerCreateInput newCustomer)
        {
            var result = await _shopifyClient.CreateCustomer.ExecuteAsync(newCustomer);
            return result.Data;
        }

        [Route("CreateCustomerAddress")]
        [HttpPost]
        public async Task<ICreateCustomerAddressResult> CreateCustomerAddress([FromBody] CreateCustomerAddressInput createCustomerAddressInput)
        {

            var result = await _shopifyClient.CreateCustomerAddress.ExecuteAsync(createCustomerAddressInput.MailingAddress,
                                                                                 createCustomerAddressInput.CustomerAccessToken);
            return result.Data;
        }

        [Route("UpdateCustomer")]
        [HttpPost]
        public async Task<IUpdateCustomerResult> UpdateCustomer([FromBody] UpdateCustomerInput updateCustomerInput)
        {

            var result = await _shopifyClient.UpdateCustomer.ExecuteAsync(updateCustomerInput.Customer,
                                                                          updateCustomerInput.CustomerAccessToken);
            return result.Data;
        }

        [Route("UpdateCustomerAddress")]
        [HttpPost]
        public async Task<IUpdateCustomerAddressResult> UpdateCustomerAddress([FromBody] UpdateCustomerAddressInput updateCustomerAddressInput)
        {

            var result = await _shopifyClient.UpdateCustomerAddress.ExecuteAsync(updateCustomerAddressInput.MailingAddress,
                                                                                 updateCustomerAddressInput.CustomerAccessToken,
                                                                                 updateCustomerAddressInput.Id);
            return result.Data;
        }

        [Route("CreateCustomerAccessToken")]
        [HttpPost]
        public async Task<ICreateCustomerAccessTokenResult> CreateCustomerAccessToken([FromBody] CustomerAccessTokenCreateInput existingCustomer)
        {
            var result = await _shopifyClient.CreateCustomerAccessToken.ExecuteAsync(existingCustomer);
            return result.Data;
        }

        [Route("CreateCheckoutLoggedIn")]
        [HttpPost]
        public async Task<ICreateCheckoutLoggedInResult> CreateCheckoutLoggedIn([FromBody] CreateCheckoutLoggedInInput checkoutInput)
        {
            var result = await _shopifyClient.CreateCheckoutLoggedIn.ExecuteAsync(checkoutInput.Email,
                                                                                  checkoutInput.LineItems,
                                                                                  checkoutInput.ShippingAddress);
            return result.Data;
        }

        [Route("CreateCheckoutLoggedOut")]
        [HttpPost]
        public async Task<ICreateCheckoutLoggedOutResult> CreateCheckoutLoggedOut([FromBody] List<CheckoutLineItemInput> lineItems)
        {
            var result = await _shopifyClient.CreateCheckoutLoggedOut.ExecuteAsync(lineItems);
            return result.Data;
        }

        [Route("CheckoutCustomerAssociate")]
        [HttpPost]
        public async Task<ICheckoutCustomerAssociateV2Result> CheckoutCustomerAssociate([FromForm] string checkoutId,
                                                                [FromForm] string customerAccessToken)
        {
            var result = await _shopifyClient.CheckoutCustomerAssociateV2.ExecuteAsync(checkoutId,
                                                                                       customerAccessToken);
            return result.Data;
        }

        
    }
}
