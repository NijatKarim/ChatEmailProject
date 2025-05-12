using ChatEmailProject.Context;
using ChatEmailProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatEmailProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly ProjectContext _projectContext;

        private readonly UserManager<AppUser> _userManager;

        public MessageController(ProjectContext projectContext, UserManager<AppUser> userManager)
        {
            _projectContext = projectContext;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Inbox(string search)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.email = values.Email;
            ViewBag.v1 = values.Name + " " + values.Surname;

            // Start with all messages for this user
            var messages = _projectContext.Messages
                .Where(x => x.RecieverEmail == values.Email);

            // If a search term is provided, filter by Subject or SenderEmail
            if (!string.IsNullOrEmpty(search))
            {
                messages = messages.Where(x =>
                    x.Subject.Contains(search) ||
                    x.SenderEmail.Contains(search));
            }

            var values2 = messages.OrderByDescending(x => x.SendDate).ToList();

            return View(values2);
        }

        public async Task<IActionResult> SendBox(string search)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string emailValue = values.Email;

            // Start with messages sent by the current user
            var sendMessageList = _projectContext.Messages
                .Where(x => x.SenderEmail == emailValue);

            // If a search term is provided, filter by receiver or subject
            if (!string.IsNullOrEmpty(search))
            {
                sendMessageList = sendMessageList.Where(x =>
                    x.RecieverEmail.Contains(search) ||
                    x.Subject.Contains(search));
            }

            return View(sendMessageList.OrderByDescending(x => x.SendDate).ToList());
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string senderEmail = values.Email;
            p.SenderEmail = senderEmail;
            p.IsRead = false;
            p.SendDate = DateTime.Now;
            _projectContext.Messages.Add(p);
            _projectContext.SaveChanges();


            return RedirectToAction("SendBox");
        }

        public async Task<IActionResult> MessageDetail(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var message =_projectContext.Messages.FirstOrDefault(x=>x.MessageID == id);
            return View(message);
        }
    }
}
