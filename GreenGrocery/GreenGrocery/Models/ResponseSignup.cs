using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace GreenGrocery.Models
{
    public class ResponseSignup
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public User Data { get; set; }
    }
}
