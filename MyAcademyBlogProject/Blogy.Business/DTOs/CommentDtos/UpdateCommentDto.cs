using Blogy.Business.DTOs.Common;

namespace Blogy.Business.DTOs.CommentDtos
{
    public class UpdateCommentDto : BaseDto
    {
        public string? Content { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsToxic { get; set; }
        public bool CommentStatus { get; set; }
    }
}
