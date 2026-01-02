using Blogy.Business.Services.AddressServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.ViewComponents.ContactViewComponents
{
    public class _AddressContactViewComponent : ViewComponent
    {
        private readonly IAddressService _addressService;

        public _AddressContactViewComponent(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _addressService.GetAllAsync();
            var value = values.FirstOrDefault();
            return View(value);
        }
    }
}
