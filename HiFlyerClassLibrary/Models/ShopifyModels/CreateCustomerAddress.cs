using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CreateCustomerAddressResponse
    {
        [JsonProperty("customerAddressCreate")]
        public CustomerAddressCreate CustomerAddressCreate { get; set; }
    }

    public partial class CustomerAddressCreate
    {
        [JsonProperty("customerAddress")]
        public DefaultAddress CustomerAddress { get; set; }

        [JsonProperty("customerUserErrors")]
        public UserErrorModel[] CustomerUserErrors { get; set; }
    }

    public partial class CreateCustomerAddressResponse
    {
        public static CreateCustomerAddressResponse FromJson(string json) => JsonConvert.DeserializeObject<CreateCustomerAddressResponse>(json, CreateCustomerAddressConverter.Settings);
    }

    public static class CreateCustomerAddressSerialize
    {
        public static string ToJson(this CreateCustomerAddressResponse self) => JsonConvert.SerializeObject(self, CreateCustomerAddressConverter.Settings);
    }

    internal static class CreateCustomerAddressConverter
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
