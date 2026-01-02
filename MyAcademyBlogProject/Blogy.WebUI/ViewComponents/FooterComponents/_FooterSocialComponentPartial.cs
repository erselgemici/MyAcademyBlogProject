using Blogy.Business.Services.SocialServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.FooterComponents
{
    public class _FooterSocialComponentPartial(ISocialService _socialService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _socialService.GetAllAsync();
            return View(values);
        }
    }
}
