using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GreenGrocery.Models
{
    public class ProductsCart
    {
        [JsonProperty("data")]
        public List<Product> Items { get; set; }

        [JsonProperty("total_quantity")]
        public int TotalQuantity { get; set; }

        [JsonProperty("total_price")]
        public double TotalPrice { get; set; }
    }
}
