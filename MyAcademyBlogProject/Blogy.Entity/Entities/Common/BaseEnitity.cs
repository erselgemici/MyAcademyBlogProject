namespace Blogy.Entity.Entities.Common
{
    public abstract class BaseEnitity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
