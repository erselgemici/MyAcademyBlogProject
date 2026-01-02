using Blogy.Business.DTOs.SocialDtos;
using Blogy.Business.Services.SocialServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SocialController(ISocialService _socialService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _socialService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSocial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocial(CreateSocialDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _socialService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSocial(int id)
        {
            await _socialService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocial(int id)
        {
            var value = await _socialService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocial(UpdateSocialDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _socialService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}
