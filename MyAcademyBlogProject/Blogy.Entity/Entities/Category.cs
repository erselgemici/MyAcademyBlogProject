using Blogy.Entity.Entities.Common;

namespace Blogy.Entity.Entities
{
    public class Category : BaseEnitity
    {
        public string Name { get; set; }

        public virtual IList<Blog> Blogs { get; set; }
    }
}
