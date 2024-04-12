using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public partial class CreateCustomerResponse
    {
        [JsonProperty("customerCreate")]
        public CustomerCreate CustomerCreate { get; set; }
    }

    public partial class CustomerCreate
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("customerUserErrors")]
        public UserErrorModel[] CustomerUserErrors { get; set; }
    }

    public partial class Customer
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; } = "-";

        [JsonProperty("acceptsMarketing")]
        public bool AcceptsMarketing { get; set; }

        [JsonProperty("defaultAddress")]
        public DefaultAddress DefaultAddress { get; set; } = new();
    }

    public partial class DefaultAddress
    {
        [JsonProperty("id")]
        public string Id { get; set; } 

        [JsonProperty("address1")]
        public string Address1 { get; set; } = "-";

        [JsonProperty("address2")]
        public string Address2 { get; set; } = "-";

        [JsonProperty("city")]
        public string City { get; set; } = "-";

        [JsonProperty("company")]
        public string Company { get; set; } = "-";

        [JsonProperty("country")]
        public string Country { get; set; } = "-";

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = "-";

        [JsonProperty("lastName")]
        public string LastName { get; set; } = "-";

        [JsonProperty("phone")]
        public string Phone { get; set; } = "-";

        [JsonProperty("province")]
        public string Province { get; set; } = "-";

        [JsonProperty("zip")]
        public string Zip { get; set; } = "-";
    }

    public partial class CreateCustomerResponse
    {
        public static CreateCustomerResponse FromJson(string json) => JsonConvert.DeserializeObject<CreateCustomerResponse>(json, CreateCustomerConverter.Settings);
    }

    public static class CreateCustomerSerialize
    {
        public static string ToJson(this CreateCustomerResponse self) => JsonConvert.SerializeObject(self, CreateCustomerConverter.Settings);
    }

    internal static class CreateCustomerConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
