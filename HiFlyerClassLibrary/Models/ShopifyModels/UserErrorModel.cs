using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFlyerClassLibrary.Models.ShopifyModels
{
    public class UserErrorModel
    {
        [JsonProperty("field")]
        public string[] Field { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
