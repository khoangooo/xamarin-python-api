using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenGrocery.Models
{
    public class Product
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }

        [JsonProperty("star")]
        public int Star { get; set; }

        [JsonProperty("kind")]
        public int Kind { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("priceMultiplyQuantity")]
        public double PriceMultiplyQuantity { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
