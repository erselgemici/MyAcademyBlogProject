using Blogy.Entity.Entities.Common;

namespace Blogy.Entity.Entities
{
    public class Comment : BaseEnitity
    {
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public bool CommentStatus { get; set; } 
        public bool IsToxic { get; set; } = false;
        public DateTime? UpdatedDate { get; set; }
    }
}
