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

    public partial class GetProductByHandleResponse
    {
        [JsonProperty("product")]
        public Product Product { get; set; }
    }

    public partial class Product
    {
        [JsonProperty("variants")]
        public Variants Variants { get; set; }

        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("priceRange")]
        public PriceRange PriceRange { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("options")]
        public Option[] Options { get; set; }
    }

    public partial class Images
    {
        [JsonProperty("edges")]
        public ImagesEdge[] Edges { get; set; }
    }

    public partial class ImagesEdge
    {
        [JsonProperty("node")]
        public PurpleNode Node { get; set; }
    }

    public partial class PurpleNode
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Option
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public string[] Values { get; set; }
    }

    public partial class PriceRange
    {
        [JsonProperty("minVariantPrice")]
        public MinVariantPrice MinVariantPrice { get; set; }
    }

    public partial class MinVariantPrice
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public partial class Variants
    {
        [JsonProperty("edges")]
        public VariantsEdge[] Edges { get; set; }
    }

    public partial class VariantsEdge
    {
        [JsonProperty("node")]
        public FluffyNode Node { get; set; }
    }

    public partial class FluffyNode
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("quantityAvailable")]
        public string Quantity { get; set; }
    }

    public partial class GetProductByHandleResponse
    {
        public static GetProductByHandleResponse FromJson(string json) => JsonConvert.DeserializeObject<GetProductByHandleResponse>(json, GetProductByHandleConverter.Settings);
    }

    public static class GetProductByHandleSerialize
    {
        public static string ToJson(this GetProductByHandleResponse self) => JsonConvert.SerializeObject(self, GetProductByHandleConverter.Settings);
    }

    internal static class GetProductByHandleConverter
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
