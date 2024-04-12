using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    

    public partial class CreateCustomerAccessTokenResponse
    {
        [JsonProperty("customerAccessTokenCreate")]
        public CustomerAccessTokenCreate CustomerAccessTokenCreate { get; set; }
    }

    public partial class CustomerAccessTokenCreate
    {
        [JsonProperty("customerAccessToken")]
        public CustomerAccessToken CustomerAccessToken { get; set; }

        [JsonProperty("customerUserErrors")]
        public object[] CustomerUserErrors { get; set; }
    }

    public partial class CustomerAccessToken
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expiresAt")]
        public DateTimeOffset ExpiresAt { get; set; }
    }

    public partial class CreateCustomerAccessTokenResponse
    {
        public static CreateCustomerAccessTokenResponse FromJson(string json) => JsonConvert.DeserializeObject<CreateCustomerAccessTokenResponse>(json, CreateCustomerAccessTokenConverter.Settings);
    }

    public static class CreateCustomerAccessTokenSerialize
    {
        public static string ToJson(this CreateCustomerAccessTokenResponse self) => JsonConvert.SerializeObject(self, CreateCustomerAccessTokenConverter.Settings);
    }

    internal static class CreateCustomerAccessTokenConverter
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
