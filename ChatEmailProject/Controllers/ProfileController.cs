using ChatEmailProject.Context;
using ChatEmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatEmailProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProjectContext _projectContext;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(ProjectContext projectContext, UserManager<AppUser> userManager)
        {
            _projectContext = projectContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> ProfileDetail()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.name = values.Name;
            ViewBag.surname = values.Surname;
            ViewBag.email = values.Email;
            ViewBag.phone = values.PhoneNumber;
            return View();
        }
    }
}
