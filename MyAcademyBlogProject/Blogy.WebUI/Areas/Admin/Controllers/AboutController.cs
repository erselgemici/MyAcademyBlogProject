using Blogy.Business.DTOs.AboutDtos;
using Blogy.Business.Services.AboutServices;
using Blogy.Business.Services.AiServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController(IAboutService _aboutService, AiArticleService _aiArticleService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _aboutService.GetAllAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _aboutService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _aboutService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var value = await _aboutService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _aboutService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GenerateFooterWithAI()
        {
            var values = await _aboutService.GetAllAsync();
            var existingAbout = values.FirstOrDefault();

            if (existingAbout == null)
            {
                TempData["Error"] = "Lütfen önce manuel bir Hakkımızda kaydı oluşturun.";
                return RedirectToAction("Index");
            }

            string aiFooterText = await _aiArticleService.GenerateFooterAboutTextAsync();

         
            var updateDto = await _aboutService.GetByIdAsync(existingAbout.Id);

            updateDto.Description2 = aiFooterText;

            await _aboutService.UpdateAsync(updateDto);
            TempData["AiResult"] = aiFooterText;
            return RedirectToAction("Index");
        }

    }
}
