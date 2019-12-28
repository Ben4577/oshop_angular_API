using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.Objects.Models
{
    public class ShoppingCart : ModelBase
    {
        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated{ get; set; }
    }
}
