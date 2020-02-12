using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace oshop_angular_API.Services
{
    public interface IIdentityService
    {
        string GetToken();
    }
}
