using AutoMapper;
using Blogy.Business.DTOs.BlogDtos;
using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.CommentServices;
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace Blogy.WebUI.Controllers
{
    public class BlogController(IBlogService _blogService,
                                ICategoryService _categoryService,
                                IMapper _mapper,
                                ICommentService _commentService,
                                UserManager<AppUser> _userManager) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
        {
            var blogs = await _blogService.GetAllAsync();

            var values = new PagedList<ResultBlogDto>(blogs.AsQueryable(), page, pageSize);

            return View(values);
        }

        public async Task<IActionResult> GetBlogsByCategory(int id, int page = 1, int pageSize = 5)
        {
            var category = await _categoryService.GetByIdAsync(id);
            ViewBag.categoryName = category.Name;
            ViewBag.categoryId = id;
            var blogs = await _blogService.GetBlogsByCategoryIdAsync(id);
            var pagedValues = new PagedList<ResultBlogDto>(blogs.AsQueryable(), page, pageSize);
            return View(pagedValues);
        }

        public async Task<IActionResult> BlogDetails(int id)
        {
            var blogs = await _blogService.GetSingleByIdAsync(id);
            return View(blogs);
        }

        
    }
}
