using Blogy.Business.DTOs.UserDtos;
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
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

            if (user == null)
            {
                // area = "" diyerek Writer klasöründen çıkıp ana dizindeki Login'e git diyoruz.
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            // Verileri taşıyacak basit bir model kullanıyoruz (EditProfileDto'yu burada da kullanabiliriz ama sadece okuma yapacağız)
            EditProfileDto model = new EditProfileDto();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ImageUrl = user.ImageUrl;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                // area = "" diyerek Writer klasöründen çıkıp ana dizindeki Login'e git diyoruz.
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            EditProfileDto model = new EditProfileDto();

            model.FirstName = user.FirstName; 
            model.LastName = user.LastName;   
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ImageUrl = user.ImageUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDto model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Resim Yükleme İşlemi
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

            // Bilgileri Güncelle
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName; // Kullanıcı adı değişirse login'e atarız
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            // Şifre kutusu doluysa şifreyi de güncelle
            if (!string.IsNullOrEmpty(model.CurrentPassword)) // Senin DTO'da CurrentPassword vardı, yeni şifre alanı varsa onu kullan
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.CurrentPassword);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // ESKİ KOD: Kullanıcıyı atıyordu
                // await _signInManager.SignOutAsync();
                // return RedirectToAction("Index", "Login", new { area = "" });

                // YENİ KOD (Professional Way): Oturumu TAZELİYORUZ
                // Kullanıcıyı atmadan, yeni bilgilerle (yeni cookie ile) tekrar giriş yaptırıyoruz.
                await _signInManager.SignInAsync(user, isPersistent: true);

                // Kullanıcıya "Başarılı" mesajı vermek istersen (Opsiyonel)
                // TempData["Success"] = "Profiliniz başarıyla güncellendi.";

                // Profil sayfasına (Görüntüleme ekranına) geri gönder
                return RedirectToAction("Index");
            }

            // Hata varsa hataları göster
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }
    }
}
