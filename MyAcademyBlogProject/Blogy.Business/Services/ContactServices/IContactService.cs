using Blogy.Business.DTOs.ContactDtos;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.ContactServices
{
    public interface IContactService : IGenericService<ResultContactDto, UpdateContactDto, SendMessageDto>
    {
      
    }
}
