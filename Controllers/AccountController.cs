using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Mult2.Data;
using Mult2.Models;
using Mult2.ViewModels;

namespace Mult2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
        if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            //var user = await _userManager.FindByLoginAsync(loginViewModel.Login, loginViewModel.Login);
           

            if (user != null) 
              {
                //find user, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Emergency");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credentials. Try again";
                return View(loginViewModel);
              }
           //user not found
            TempData["Error"] = "Wrong credentials. Try again";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);//error SqlException: Invalid object name 'AspNetUsers'.
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);//?

            if (newUserResponse.Succeeded)


            //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var roleExist = await RoleManager.RoleExistsAsync("Admin");
            //if (!roleExist)
            //{
            //    roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            //}
            //await _userManager.AddToRoleAsync(user, "Admin");


            //var serviceScope = _userManager.ApplicationServices.CreateScope();
            //var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));



            await _userManager.AddToRoleAsync(newUser, "user");//?admin?
            //return View("Register");
            return RedirectToAction("Index", "Emergency");//old
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
        await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Emergency");
        }
    }
}
