using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace oshop_angular_API.Models
{
    public class ShoppingCart
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated{ get; set; }
    }
}
