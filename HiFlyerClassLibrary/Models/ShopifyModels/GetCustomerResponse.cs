using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    

    public partial class GetCustomerResponse
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }

    public partial class GetCustomerResponse
    {
        public static GetCustomerResponse FromJson(string json) => JsonConvert.DeserializeObject<GetCustomerResponse>(json, GetCustomerConverter.Settings);
    }


    public static class GetCustomerSerialize
    {
        public static string ToJson(this GetCustomerResponse self) => JsonConvert.SerializeObject(self, GetCustomerConverter.Settings);
    }

    internal static class GetCustomerConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
