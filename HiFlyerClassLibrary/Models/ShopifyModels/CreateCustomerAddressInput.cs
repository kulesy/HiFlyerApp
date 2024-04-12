using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public class CreateCustomerAddressInput
    {
        public MailingAddressInput MailingAddress { get; set; }
        public string CustomerAccessToken { get; set; }
    }
}
