using Blogy.Business.Services.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.FooterComponents
{
    public class _FooterAboutComponentPartial(IAboutService _aboutService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _aboutService.GetAllAsync();
            var value =values.FirstOrDefault();
            return View(value);
        }
    }
}
