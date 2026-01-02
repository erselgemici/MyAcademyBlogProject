using Blogy.Business.Services.BlogServices;
using Blogy.Business.Services.CategoryServices;
using Blogy.Business.Services.CommentServices;
using Blogy.Business.Services.ContactServices;
using Blogy.Business.Services.TagServices; // TagService eklemeyi unutma
using Blogy.Entity.Entities;
using Blogy.WebUI.Areas.Admin.Models; // ViewModel namespace'ini ekle
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly ITagService _tagService;
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(IBlogService blogService, ICategoryService categoryService, ICommentService commentService, ITagService tagService, IContactService contactService, UserManager<AppUser> userManager)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _commentService = commentService;
            _tagService = tagService;
            _contactService = contactService;
            _userManager = userManager;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();

            var blogs = await _blogService.GetBlogsWithCategoriesAsync(); 
            var categories = await _categoryService.GetAllAsync();
            var tags = await _tagService.GetAllAsync();
            var comments = await _commentService.GetAllAsync();
            var messages = await _contactService.GetAllAsync();
            var users = _userManager.Users.ToList();

            model.BlogCount = blogs.Count;
            model.CategoryCount = categories.Count;
            model.TagCount = tags.Count;
            model.CommentCount = comments.Count;
            model.MessageCount = messages.Count;
            model.UserCount = users.Count;

            var lastBlog = blogs.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            model.LastBlogTitle = lastBlog != null ? lastBlog.Title : "Henüz blog yok";

            var lastComment = comments.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            model.LastCommentContent = lastComment != null
                ? (lastComment.Content.Length > 40 ? lastComment.Content.Substring(0, 40) + "..." : lastComment.Content)
                : "Henüz yorum yok";

            var lastUser = users.OrderByDescending(x => x.Id).FirstOrDefault();
            model.LastUserEmail = lastUser != null ? lastUser.Email : "Henüz üye yok";

            var categoryStats = blogs.Where(x => x.CategoryId != null)
                                     .GroupBy(x => x.CategoryId)
                                     .Select(g => new {
                                         Name = categories.FirstOrDefault(c => c.Id == g.Key)?.CategoryName ?? "Diğer",
                                         Count = g.Count()
                                     })
                                     .ToList();

            model.ChartCategoryNames = categoryStats.Select(x => x.Name).ToList();
            model.ChartCategoryCounts = categoryStats.Select(x => x.Count).ToList();

            var writerStats = blogs.Where(x => x.WriterId != null)
                                   .GroupBy(x => x.WriterId)
                                   .Select(g => new {
                                       Name = users.FirstOrDefault(u => u.Id == g.Key)?.UserName ?? "Bilinmiyor",
                                       Count = g.Count()
                                   })
                                   .OrderByDescending(x => x.Count)
                                   .Take(5)
                                   .ToList();

            model.ChartWriterNames = writerStats.Select(x => x.Name).ToList();
            model.ChartWriterCounts = writerStats.Select(x => x.Count).ToList();

            model.LastFiveBlogs = blogs.OrderByDescending(x => x.CreatedDate).Take(5).ToList();

            return View(model);
        }
    }
}
