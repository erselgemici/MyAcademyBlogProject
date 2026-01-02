using Blogy.Business.DTOs.CommentDtos;
using Blogy.Business.Services.AiServices; // AI Servisi
using Blogy.Business.Services.CommentServices;
using Blogy.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogy.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AiContentModerator _aiModerator; 

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
            _aiModerator = new AiContentModerator(); 
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto p)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                p.UserId = user.Id;
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            bool isToxic = await _aiModerator.IsContentToxicAsync(p.Content);

            if (isToxic)
            {
                TempData["ErrorMessage"] = "Yorumunuz kaba veya uygunsuz dil içerdiği için reddedildi!";

                return RedirectToAction("BlogDetails", "Blog", new { id = p.BlogId });
            }

            p.IsToxic = false;
            p.CommentStatus = true;

            await _commentService.CreateAsync(p);

            TempData["SuccessMessage"] = "Yorumunuz başarıyla yayınlandı.";
            return RedirectToAction("BlogDetails", "Blog", new { id = p.BlogId });
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetByIdAsync(id); // Önce yorumu bul
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Giriş yapan kullanıcıyı bul

            // Yorumu silmeye çalışan kişi, yorumun sahibi mi?
            if (comment.UserId == user.Id)
            {
                await _commentService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Yorumunuz silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Bu yorumu silmeye yetkiniz yok!";
            }

            return RedirectToAction("BlogDetails", "Blog", new { id = comment.BlogId });
        }

        
    }
}
