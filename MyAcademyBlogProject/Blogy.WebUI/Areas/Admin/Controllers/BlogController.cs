using Blogy.Business.DTOs.BlogDtos;
using Blogy.Business.Services.AiServices;
using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.TagServices;
using Blogy.Entity.Entities;
using Blogy.WebUI.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Roles.Admin}")]
    public class BlogController(IBlogService _blogService, ICategoryService _categoryService, UserManager<AppUser> _userManager,
                                ITagService _tagService, AiArticleService _aiArticleService) : Controller
    {
        private async Task GetCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            ViewBag.categories = (from category in categories
                                  select new SelectListItem
                                  {
                                      Text = category.CategoryName,
                                      Value = category.Id.ToString()
                                  }).ToList();
        }

        private async Task GetTagsAsyns()
        {
            var tagList = await _tagService.GetAllAsync();
            List<SelectListItem> tags = (from x in tagList
                                         select new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.tags = tags;
        }
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            return View(blogs);
        }

        public async Task<IActionResult> CreateBlog()
        {
            await GetCategoriesAsync();
            await GetTagsAsyns();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCategoriesAsync();
                return View(createBlogDto);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createBlogDto.WriterId = user.Id;
            await _blogService.CreateAsync(createBlogDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateBlog(int id)
        {
            await GetCategoriesAsync();
            await GetTagsAsyns();
            var blog = await _blogService.GetByIdAsync(id);
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            if (!ModelState.IsValid)
            {
                await GetCategoriesAsync();
                return View(updateBlogDto);
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            updateBlogDto.WriterId = user.Id;
            await _blogService.UpdateAsync(updateBlogDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // 1. Güvenlik tokenı zorunluluğunu kaldırıyoruz (AJAX için)
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GenerateAiArticle([FromBody] GenerateBlogAiDto model)
        {
            if (string.IsNullOrEmpty(model.Keywords) || string.IsNullOrEmpty(model.Prompt))
            {
                return BadRequest("Lütfen anahtar kelime ve konu giriniz.");
            }

            string article = await _aiArticleService.GenerateArticleAsync(model.Keywords, model.Prompt);

            return Json(new { content = article });
        }
    }
}
