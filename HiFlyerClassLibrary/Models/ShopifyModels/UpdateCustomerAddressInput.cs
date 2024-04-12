using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public class UpdateCustomerAddressInput
    {
        public MailingAddressInput MailingAddress { get; set; }
        public string CustomerAccessToken { get; set; }
        public string Id { get; set; }
    }
}
