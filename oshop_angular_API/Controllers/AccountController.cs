using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using oshop_angular_API.Models.Identity;

namespace oshop_angular_API.Controllers
{
   
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult LoggedOn()
        {
            return View();
        }



        [AllowAnonymous]
        //[Route("login")]
        [HttpGet]
        public ViewResult Login()
        {
            var model = new LoginModel();

            return View(model);
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


        [AllowAnonymous]
        [HttpPost]
        //[Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid name or password");
                return View("Login", loginModel);
            }

            User user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user == null)
            {
                //Register
                user = new User
                {
                    UserName = loginModel.UserName,
                    Email = loginModel.UserName,
                    FirstName = "John",
                    LastName = "Richards",
                };

                IdentityResult result = await _userManager.CreateAsync(user, loginModel.Password);

                if(result.Succeeded)
                {
                    ViewBag.NewMessage = "You have been Registered";

                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/Account/Index");
                    }
                }

            }
            else
            {
                await _signInManager.SignOutAsync();

                if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                {
                    return Redirect("/Account/LoggedOn");
                }
            }


            return View("Login", loginModel);

        }


        //public async Task<RedirectResult> Logout(string returnUrl = "/")
        //{
        //    await _signInManager.SignOutAsync();
        //    return Redirect(returnUrl);
        //}



    }
}