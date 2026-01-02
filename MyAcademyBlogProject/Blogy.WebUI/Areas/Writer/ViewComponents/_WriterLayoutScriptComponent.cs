using Microsoft.AspNetCore.Mvc;
namespace Blogy.WebUI.Areas.Writer.ViewComponents
{
    public class _WriterLayoutScriptComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
