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

    public partial class UpdateCustomerResponse
    {
        [JsonProperty("customerUpdate")]
        public CustomerUpdate CustomerUpdate { get; set; }
    }

    public partial class CustomerUpdate
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("customerAccessToken")]
        public CustomerAccessToken CustomerAccessToken { get; set; }

        [JsonProperty("customerUserErrors")]
        public UserErrorModel[] CustomerUserErrors { get; set; }
    }

    public partial class UpdateCustomerResponse
    {
        public static UpdateCustomerResponse FromJson(string json) => JsonConvert.DeserializeObject<UpdateCustomerResponse>(json, UpdateCustomerConverter.Settings);
    }

    public static class UpdateCustomerSerialize
    {
        public static string ToJson(this UpdateCustomerResponse self) => JsonConvert.SerializeObject(self, UpdateCustomerConverter.Settings);
    }

    internal static class UpdateCustomerConverter
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
