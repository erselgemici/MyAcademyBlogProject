using Blogy.Business.Services.TagServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogy.WebUI.ViewComponents.TagListSidebarComponent
{
    public class _TagListSidebarComponent : ViewComponent
    {
        private readonly ITagService _tagService;

        public _TagListSidebarComponent(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _tagService.GetAllAsync();
            return View(values);
        }
    }
}
