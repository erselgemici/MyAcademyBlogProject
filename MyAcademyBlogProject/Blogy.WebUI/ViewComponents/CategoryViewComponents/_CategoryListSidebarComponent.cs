using Blogy.Business.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.CategoryViewComponents
{
    public class _CategoryListSidebarComponent(ICategoryService _categoryService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var values = _categoryService.GetCategoriesWithCount();
            return View(values);
        }
    }
}
