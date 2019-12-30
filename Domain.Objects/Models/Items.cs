using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Objects.Models
{
    public class Items
    {
        [JsonProperty(PropertyName = "productOrder")]
        public ProductOrder ProductOrder { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "totalPrice")]
        public decimal TotalPrice { get; set; }

    }
}
