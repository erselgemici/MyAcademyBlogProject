using AutoMapper;
using Blogy.Business.DTOs.AddressDtos;
using Blogy.Entity.Entities;

namespace Blogy.Business.Mappings
{
    public class AddressMappings : Profile
    {
        public AddressMappings()
        {
            CreateMap<Address, ResultAddressDto>().ReverseMap();
            CreateMap<Address, UpdateAddressDto>().ReverseMap();
            CreateMap<Address, CreateAddressDto>().ReverseMap();
            CreateMap<UpdateAddressDto, ResultAddressDto>().ReverseMap();
        }
    }
}
