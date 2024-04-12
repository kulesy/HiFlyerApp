using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public partial class CreateCheckoutResponse
    {
        [JsonProperty("checkoutCreate")]
        public CheckoutCreate CheckoutCreate { get; set; }
    }

    public partial class CheckoutCreate
    {
        [JsonProperty("checkout")]
        public Checkout Checkout { get; set; }

        [JsonProperty("checkoutUserErrors")]
        public UserErrorModel[] CheckoutUserErrors { get; set; }
    }

    public partial class Checkout
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("webUrl")]
        public Uri WebUrl { get; set; }
    }

    public partial class CreateCheckoutResponse
    {
        public static CreateCheckoutResponse FromJson(string json) => JsonConvert.DeserializeObject<CreateCheckoutResponse>(json, CreateCheckoutConverter.Settings);
    }

    public static class CreateCheckoutSerialize
    {
        public static string ToJson(this CreateCheckoutResponse self) => JsonConvert.SerializeObject(self, CreateCheckoutConverter.Settings);
    }

    internal static class CreateCheckoutConverter
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
