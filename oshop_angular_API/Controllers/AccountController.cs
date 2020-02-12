using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models.Identity;
using oshop_angular_API.Services;

namespace oshop_angular_API.Controllers
{

    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,
            IIdentityService identityService)
        {
            _identityService = identityService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            if (loginModel.UserName.IsNullEmptyOrWhiteSpace() || loginModel.Password.IsNullEmptyOrWhiteSpace())
            {
                return BadRequest();
            }

            User user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
            {
                return BadRequest();
            }

            await _signInManager.SignOutAsync();

            if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
            {
                ReturnUser returnUser = new ReturnUser
                {
                    FullName = user.FirstName + ' ' + user.LastName,
                    Token = _identityService.GetToken()
                };
                    return Ok(returnUser);
            }
            return BadRequest();
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("getBearerToken")]
        public IActionResult GetBearerToken()
        { 
            var token = _identityService.GetToken();
            return Ok(token);
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(true);
        }



        //[HttpPost("register")]
        //public async Task<IActionResult> Register(LoginModel loginModel)
        //{
        //    try
        //    {
        //        User user = await _userManager.FindByNameAsync(loginModel.UserName);

        //        if (user == null)
        //        {
        //            user = new User
        //            {
        //                UserName = loginModel.UserName,
        //                Email = loginModel.UserName,
        //                Password = loginModel.Password,
        //                FirstName = "John",
        //                LastName = "Doe",
        //            };

        //            IdentityResult result = await _userManager.CreateAsync(user, user.Password);
        //            ViewBag.NewMessage = "You have been Registered";

        //            if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
        //            {
        //                return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.NewMessage = "User already registered";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = ex.Message;
        //    }

        //    return View("Login", loginModel);
        //}


    }
}