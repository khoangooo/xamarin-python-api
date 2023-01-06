using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GreenGrocery.Models
{
    public class ResponseData
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<Product> Data { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonIgnore]
        [JsonProperty("limit", Required = Newtonsoft.Json.Required.AllowNull)]
        public int Limit { get; set; }

        [JsonIgnore]
        [JsonProperty("offset", Required = Newtonsoft.Json.Required.AllowNull)]
        public int Offset { get; set; }

    }
}
