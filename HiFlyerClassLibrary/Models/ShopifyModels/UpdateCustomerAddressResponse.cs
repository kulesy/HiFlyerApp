using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public partial class UpdateCustomerAddressResponse
    {
        [JsonProperty("customerAddressUpdate")]
        public CustomerAddressUpdate CustomerAddressUpdate { get; set; }
    }

    public partial class CustomerAddressUpdate
    {
        [JsonProperty("customerAddress")]
        public DefaultAddress CustomerAddress { get; set; }

        [JsonProperty("customerUserErrors")]
        public UserErrorModel[] CustomerUserErrors { get; set; }
    }

    public partial class UpdateCustomerAddressResponse
    {
        public static UpdateCustomerAddressResponse FromJson(string json) => JsonConvert.DeserializeObject<UpdateCustomerAddressResponse>(json, UpdateCustomerAddressConverter.Settings);
    }

    public static class UpdateCustomerAddressSerialize
    {
        public static string ToJson(this UpdateCustomerAddressResponse self) => JsonConvert.SerializeObject(self, UpdateCustomerAddressConverter.Settings);
    }

    internal static class UpdateCustomerAddressConverter
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
