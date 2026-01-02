using Blogy.Business.DTOs.ContactDtos;
using Blogy.Business.Services.AiServices;
using Blogy.Business.Services.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Controllers
{
    public class ContactController(IContactService _contactService, AiArticleService _aiArticleService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SendMessageDto sendMessageDto)
        {
            if (ModelState.IsValid)
            {
                sendMessageDto.CreatedDate = DateTime.Now; 
                sendMessageDto.IsRead = false;           

                await _contactService.CreateAsync(sendMessageDto);

                // Kullanıcının mesaj içeriğini (Message) gönderiyoruz.
                string autoReply = await _aiArticleService.GenerateContactReplyAsync(sendMessageDto.Message);

                TempData["AiReply"] = autoReply; // AI cevabını buraya koyduk

                return RedirectToAction("Index");
            }

            return View(sendMessageDto);
        }
    }
}
