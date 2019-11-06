using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace oshop_angular_API.Models
{
    public class Product
    { 
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
        
        [JsonProperty(PropertyName = "imageURL")]
        public string ImageURL { get; set; }

    }
}
