using AutoMapper;
using HiFlyer.HiFlyerClassLibrary.GraphQLAPIClient;
using HiFlyerClassLibrary.Models.ShopifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerUpdateInput>();
            CreateMap<CustomerUpdateInput, Customer>();
        }
    }
}
