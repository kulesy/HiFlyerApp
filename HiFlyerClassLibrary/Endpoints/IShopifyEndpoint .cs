using HiFlyerClassLibrary.Models.ShopifyModels;
using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using HiFlyerClassLibrary.Models;
using HiFlyerClassLibrary.Models.Authentication;

namespace HiFlyerClassLibrary.Endpoints
{
    public interface IShopifyEndpoint
    {
        Task<CheckoutCustomerAssociateResponse> CheckoutCustomerAssociate(string checkoutId, string customerAccessToken);
        Task<CreateCheckoutResponse> CreateCheckoutLoggedIn(List<CartProduct> cart, Customer customer);
        Task<CreateCheckoutResponse> CreateCheckoutLoggedOut(List<CartProduct> cart);
        Task<CreateCustomerResponse> CreateCustomer(RegisterModel newCustomer);
        Task<CreateCustomerAccessTokenResponse> CreateCustomerAccessToken(LoginModel existingCustomer);
        Task<CreateCustomerAddressResponse> CreateCustomerAddress(Customer customer, string customerAccessToken);
        Task<GetCustomerResponse> GetCustomer(string token);
        Task<GetProductByHandleResponse> GetProductByHandle(string handle);
        Task<GetProductsResponse> GetProducts();
        Task<UpdateCustomerResponse> UpdateCustomer(Customer customer, string customerAccessToken);
        Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(Customer customer, string customerAccessToken);
    }
}