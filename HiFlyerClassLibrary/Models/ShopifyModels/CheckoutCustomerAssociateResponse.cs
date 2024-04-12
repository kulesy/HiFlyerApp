using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{


    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CheckoutCustomerAssociateResponse
    {
        [JsonProperty("checkoutCustomerAssociateV2")]
        public CheckoutCustomerAssociateV2 CheckoutCustomerAssociateV2 { get; set; }
    }

    public partial class CheckoutCustomerAssociateV2
    {
        [JsonProperty("checkout")]
        public Checkout Checkout { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("userErrors")]
        public UserErrorModel[] UserErrors { get; set; }
    }

    public partial class CheckoutCustomerAssociateResponse
    {
        public static CheckoutCustomerAssociateResponse FromJson(string json) => JsonConvert.DeserializeObject<CheckoutCustomerAssociateResponse>(json, CheckoutCustomerAssociateConverter.Settings);
    }

    public static class CheckoutCustomerAssociateSerialize
    {
        public static string ToJson(this CheckoutCustomerAssociateResponse self) => JsonConvert.SerializeObject(self, CheckoutCustomerAssociateConverter.Settings);
    }

    internal static class CheckoutCustomerAssociateConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
