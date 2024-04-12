using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public class UpdateCustomerInput
    {
        public CustomerUpdateInput Customer { get; set; }
        public string CustomerAccessToken { get; set; }
    }
}
