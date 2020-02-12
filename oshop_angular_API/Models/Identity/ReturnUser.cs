using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace oshop_angular_API.Models.Identity
{
    public class ReturnUser
    {
        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

    }
}
