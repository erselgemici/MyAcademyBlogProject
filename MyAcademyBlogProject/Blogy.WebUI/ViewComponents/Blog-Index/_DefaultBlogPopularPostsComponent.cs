using Blogy.Business.Services.BlogServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.Blog_Index
{
    public class _DefaultBlogPopularPostsComponent(IBlogService _blogService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _blogService.GetLastNBlogsAsync(3);
            return View(values);
        }
    }
}
