using Blogy.Business.DTOs.UserDtos; // DTO'nun olduğu namespace
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    public class ChangePasswordController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ChangePasswordController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Kullanıcı oturumu düşmüşse login'e at
            if (user == null)
            {
                return RedirectToAction("Index", "Login", new { area = "" });
            }

            // Şifre Değiştirme İşlemi
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Admin tarafında çıkış yaptırıyordun, burada kullanıcıyı yormayalım, oturumu yenileyelim.
                // Eğer "Hayır admin gibi çıkış yapsın" dersen burayı _signInManager.SignOutAsync() yapabilirsin.
                await _signInManager.SignInAsync(user, isPersistent: true);

                // Başarılı olunca Profil sayfasına veya Dashboard'a yönlendir
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                // Hata varsa (Örn: Eski şifre yanlış, Yeni şifre kurallara uymuyor)
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }
    }
}
