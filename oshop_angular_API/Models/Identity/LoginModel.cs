using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataAnnotationsExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace oshop_angular_API.Models.Identity
{
    public class LoginModel
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        //public string ReturnUrl { get; set; } = "/";

    }
}
