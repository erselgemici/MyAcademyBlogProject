using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.User.ViewComponents
{
    public class _UserHeadComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
