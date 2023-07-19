using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl   
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await _userManger.FindByNameAsync(model.Name);
                if (user is not null) 
                {
                    await _signInManager.SignOutAsync();//aktif oturum varsa sonlandı
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model.ReturnUrl ?? "/");
                        //returUrl varsa oraya gitcek aksi halde ana sayfaya gitcek
                        //mesela alışverişi yaptı ödeme gitcek önce logine sonra ödemeye gitcek
                    }
                }
                ModelState.AddModelError("Error", "Invalid username or password");
            }
            return View();
        }




        public async Task<IActionResult> LogOut([FromQuery(Name = "ReturnUrl")] string ReturnUrl="/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,

            };

            var result = await _userManger
                .CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                var roleResult = await _userManger
                    .AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                    return RedirectToAction("Login",new {ReturnUrl="/"});
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View();
        }

        
        public IActionResult AccessDenied([FromQuery(Name ="ReturnUrl")] string returnUrl)//erişim engellenince sayfada üretilen isimle aynı
        {
            return View();
        }

    }
}
