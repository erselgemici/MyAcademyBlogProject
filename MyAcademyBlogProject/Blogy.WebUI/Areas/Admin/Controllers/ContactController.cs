using Blogy.Business.DTOs.ContactDtos;
using Blogy.Business.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController(IContactService _contactService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _contactService.GetAllAsync();
            var sortedValues = values.OrderByDescending(x => x.CreatedDate).ToList();
            return View(sortedValues); 
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteAsync(id);
            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }

        
    }
}
