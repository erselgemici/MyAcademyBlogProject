using Blogy.Business.DTOs.TagDtos;
using Blogy.Business.Services.TagServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TagController(ITagService _tagService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var values = await _tagService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _tagService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTag(int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTag(int id)
        {
            var value = await _tagService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTag(UpdateTagDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await _tagService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}
