using Learning_ASP.NET_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP.NET_MVC_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await _userManager.Users
                .Where(u => u.Email != "admin@gmail.com") 
                .ToListAsync();

            var userViewModel = new List<UserViewModels>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModel.Add(new UserViewModels()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            var dashboardViewModels = new DashboardViewModels()
            {
                Users = userViewModel,
                TotalUsers = users.Count 
            };
            return View(dashboardViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var managerUserViewModels = new ManageUserViewModels
            {
                RoleList = _roleManager.Roles
                    .Select( r => new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Name
                    })
            };
            return View(managerUserViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManageUserViewModels model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser{UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.RoleId))
                    {
                        await _userManager.AddToRoleAsync(user, model.RoleId);
                    }

                    return RedirectToAction("Dashboard");
                }

                foreach (var error in result.Errors )
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.RoleList = _roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");
            // Protect Admin for Delete own
            var currentUser = await _userManager.GetUserAsync(User);
            if (user.Id == currentUser?.Id)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction("Dashboard");
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Dashboard");
        }
        
        
        
    }
}

