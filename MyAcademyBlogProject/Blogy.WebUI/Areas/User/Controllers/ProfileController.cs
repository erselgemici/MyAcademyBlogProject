using Blogy.Business.DTOs.UserDtos;
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.User.Controllers
{
    [Area("User")] // Burası User Alanı
    [Route("User/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // 1. EKRAN: SADECE GÖRÜNTÜLEME (Read-Only)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Güvenlik Kontrolü: Kullanıcı bulunamazsa ana giriş sayfasına at
            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            // Verileri görüntüleme modeline aktar
            EditProfileDto model = new EditProfileDto();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ImageUrl = user.ImageUrl;

            return View(model);
        }

        // 2. EKRAN: DÜZENLEME SAYFASI (Formu Dolu Getirir)
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Güvenlik Kontrolü
            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            // Kutuların içi boş gelmesin diye verileri dolduruyoruz
            EditProfileDto model = new EditProfileDto();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ImageUrl = user.ImageUrl;

            return View(model);
        }

        // 3. İŞLEM: GÜNCELLEME (POST)
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            // --- RESİM YÜKLEME İŞLEMİ ---
            if (model.ImageFile != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.ImageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/userimages/" + imageName;

                var stream = new FileStream(saveLocation, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                user.ImageUrl = "/userimages/" + imageName;
            }

            // --- BİLGİLERİ GÜNCELLE ---
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            // Şifre kutusu doluysa şifreyi de hashleyip güncelle
            if (!string.IsNullOrEmpty(model.CurrentPassword))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.CurrentPassword);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // ÖNEMLİ: Kullanıcıyı atmadan oturumunu tazele (Refresh Login)
                await _signInManager.SignInAsync(user, isPersistent: true);

                // İşlem bitince Profil sayfasına geri dön
                return RedirectToAction("Index");
            }

            // Hata varsa sayfada göster
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }
    }
}
