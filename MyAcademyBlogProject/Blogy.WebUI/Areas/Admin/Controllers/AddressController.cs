using Blogy.Business.DTOs.AddressDtos;
using Blogy.Business.Services.AddressServices;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _addressService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
        {
            // Mapping yok, direkt DTO'yu servise atıyoruz.
            await _addressService.CreateAsync(createAddressDto);
            return RedirectToAction("Index", "Address", new { area = "Admin" });
        }


        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _addressService.DeleteAsync(id);
            return RedirectToAction("Index", "Address", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAddress(int id)
        {
            var value = await _addressService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            await _addressService.UpdateAsync(updateAddressDto);
            return RedirectToAction("Index", "Address", new { area = "Admin" });
        }
    }
}
