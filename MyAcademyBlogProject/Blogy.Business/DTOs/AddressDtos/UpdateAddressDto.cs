namespace Blogy.Business.DTOs.AddressDtos
{
    public class UpdateAddressDto
    {
        public int Id { get; set; }
        public string AddressDetail { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string MapLocation { get; set; }
    }
}
