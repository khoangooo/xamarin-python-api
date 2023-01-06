using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GreenGrocery.Models
{
    public class ResponseData
    {
        [JsonProperty("data")]
        public List<Product> Data { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

    }
}
