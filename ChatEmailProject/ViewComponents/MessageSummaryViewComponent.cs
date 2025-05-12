using ChatEmailProject.Context;
using ChatEmailProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatEmailProject.ViewComponents
{
    public class MessageSummaryViewComponent: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ProjectContext _context;

        public MessageSummaryViewComponent(UserManager<AppUser> userManager, ProjectContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null) return View("Default", (0, 0));

            var inboxCount = _context.Messages.Count(x => x.RecieverEmail == user.Email);
            var sentCount = _context.Messages.Count(x => x.SenderEmail == user.Email);

            return View("Default", (inboxCount, sentCount));
        }
    }
}
