using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public class CreateCheckoutLoggedInInput
    {
        public string Email { get; set; }
        public List<CheckoutLineItemInput> LineItems { get; set; }
        public MailingAddressInput ShippingAddress { get; set; }
    }
}
