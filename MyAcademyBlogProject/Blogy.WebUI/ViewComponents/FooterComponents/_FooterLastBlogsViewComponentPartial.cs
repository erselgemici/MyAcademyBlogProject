using Blogy.Business.Services.BlogServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.FooterComponents
{
    public class _FooterLastBlogsViewComponentPartial(IBlogService _blogService) :  ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _blogService.GetAllAsync();
           
            var last3Blogs = values.OrderByDescending(x => x.CreatedDate).Take(3).ToList();

            return View(last3Blogs);
        }
    }
}
