using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace GreenGrocery.Models
{
    public class Category
    {
        [JsonIgnore]
        [PrimaryKey]
        public int Id { get; set; }

        [JsonProperty("kind")]
        public int Kind { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
