using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public partial class GetProductsResponse
    {
        [JsonProperty("products")]
        public Products Products { get; set; }
    }

    public partial class Products
    {
        [JsonProperty("edges")]
        public ProductsEdge[] Edges { get; set; }
    }

    public partial class ProductsEdge
    {
        [JsonProperty("node")]
        public Product Node { get; set; }
    }

    public partial class GetProductsResponse
    {
        public static GetProductsResponse FromJson(string json) => JsonConvert.DeserializeObject<GetProductsResponse>(json, GetProductsConverter.Settings);
    }

    public static class GetProductsSerialize
    {
        public static string ToJson(this GetProductsResponse self) => JsonConvert.SerializeObject(self, GetProductsConverter.Settings);
    }

    internal static class GetProductsConverter
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
