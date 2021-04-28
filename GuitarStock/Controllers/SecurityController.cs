using GuitarStock.Models;
using GuitarStock.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Controllers
{
    public class SecurityController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> userManager;
        private readonly RoleManager<ApplicationIdentityRole> roleManager;
        private readonly SignInManager<ApplicationIdentityUser> signinManager;

        public SecurityController(UserManager<ApplicationIdentityUser> userManager, 
                                  RoleManager<ApplicationIdentityRole> roleManager,
                                  SignInManager<ApplicationIdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }

        //Get Register
        public IActionResult Register()
        {
            return View();
        }

    // Post Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    ApplicationIdentityRole role = new ApplicationIdentityRole();
                    role.Name = "Admin";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }

                ApplicationIdentityUser user = new ApplicationIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;

                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    return RedirectToAction("Login", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "User details were invalid");
                }
            }
            return View(obj);
        }
    
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Signin s)
        {
            if(ModelState.IsValid)
            {
                var result = signinManager.PasswordSignInAsync(
                    s.UserName, s.Password, s.RememberMe, false).Result;

                if(result.Succeeded) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("", "Login credentials invalid");
            }

            return View(s);
        }

        public IActionResult Logout()
        {
            var result = signinManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPassword)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
