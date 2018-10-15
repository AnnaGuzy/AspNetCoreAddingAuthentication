using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController
            (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if(ModelState.IsValid == false)
            {
                return View(vm);
            }
            var task = await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email
            }, 
            vm.Password);

            if(task.Succeeded == false)
            {
                foreach (var item in task.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }    
}
