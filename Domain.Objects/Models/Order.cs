using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Objects.Models
{ 
    public class Order : ModelBase
    {
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "datePlaced")]
        public DateTime DatePlaced  { get; set; }

        [JsonProperty(PropertyName = "shipping")]
        public int Shipping { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<Items> Items { get; set; }
    }
}
