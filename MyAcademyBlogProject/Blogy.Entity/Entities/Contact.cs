using Blogy.Entity.Entities.Common;

namespace Blogy.Entity.Entities
{
    public class Contact : BaseEnitity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }
}
