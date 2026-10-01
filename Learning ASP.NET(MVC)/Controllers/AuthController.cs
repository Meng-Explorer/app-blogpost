using Learning_ASP.NET_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learning_ASP.NET_MVC_.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public AuthController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModels registerViewModels)
    {
        // check for Validation
        if (ModelState.IsValid)
        {
            // Create Identify User Object 
            var user = new IdentityUser { UserName = registerViewModels.Email, Email = registerViewModels.Email };
            // Create User
            var result =  await _userManager.CreateAsync(user, registerViewModels.Password!);
            // If User Created Success 
            if (result.Succeeded)
            {
                // If the User Role Exist in DataBase
                if(!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index","Post");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
        }
        return View(registerViewModels);
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModels loginViewModels)
    {
        if (ModelState.IsValid)
        {
            var user = await  _userManager.FindByEmailAsync(loginViewModels.Email!);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
                return View(loginViewModels);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user!, loginViewModels.Password!, false, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
                return View(loginViewModels);
            }
            return RedirectToAction("Index", "Post");
        }
        
        return View(loginViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Post");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
    
}