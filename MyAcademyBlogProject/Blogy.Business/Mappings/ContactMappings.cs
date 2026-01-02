using AutoMapper;
using Blogy.Business.DTOs.ContactDtos;
using Blogy.Entity.Entities;

namespace Blogy.Business.Mappings
{
    public class ContactMappings : Profile
    {
        public ContactMappings()
        {
            CreateMap<SendMessageDto, Contact>().ReverseMap();
            CreateMap<ResultContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();
        }
    }
}
