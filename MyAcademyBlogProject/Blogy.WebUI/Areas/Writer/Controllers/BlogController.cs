using Blogy.Business.DTOs.BlogDtos;
using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Entity.Entities; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogy.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Writer")]
    public class BlogController(IBlogService _blogService, UserManager<AppUser> _userManager,
                                ICategoryService _categoryService) : Controller
    {
        
        public async Task<IActionResult> MyBlogList()
        {
            // 1. Giriş yapan kullanıcıyı bul
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // 2. Servisten tüm blogları kategorileriyle çek (ResultBlogDto listesi döner)
            var allBlogs = await _blogService.GetBlogsWithCategoriesAsync();

            // 3. Filtreleme: Yazar ID'si eşleşenleri al.
            var writerBlogs = allBlogs.Where(x => x.WriterId == user.Id).ToList();

            return View(writerBlogs);
        }

        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            var categories = await _categoryService.GetAllAsync();

            List<SelectListItem> categoryValues = (from x in categories
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.Id.ToString()
                                                   }).ToList();

            ViewBag.cv = categoryValues;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(CreateBlogDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            model.CreatedDate = DateTime.Now;
            model.WriterId = user.Id;         

            await _blogService.CreateAsync(model);

            return RedirectToAction("MyBlogList");
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _blogService.DeleteAsync(id);

            return RedirectToAction("MyBlogList");
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var categories = await _categoryService.GetAllAsync();
            List<SelectListItem> categoryValues = (from x in categories
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;

            var values = await _blogService.GetByIdAsync(id);

            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(UpdateBlogDto model)
        {
            await _blogService.UpdateAsync(model);

            return RedirectToAction("MyBlogList");
        }
    }
}
