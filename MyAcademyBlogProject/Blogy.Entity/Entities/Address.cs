using Blogy.Entity.Entities.Common;

namespace Blogy.Entity.Entities
{
    public class Address : BaseEnitity
    {
        public string AddressDetail { get; set; } 
        public string Phone { get; set; }    
        public string Email { get; set; }    
        public string Description { get; set; }  
        public string MapLocation { get; set; }
    }
}
