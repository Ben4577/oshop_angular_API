using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Objects.Models
{
    public class ModelBase
    {

        [JsonProperty("id")]
        public string Id { get; set; }


        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string TypeName => GetType().Name;



    }
}
