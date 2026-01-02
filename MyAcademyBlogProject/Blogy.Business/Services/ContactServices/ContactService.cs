using AutoMapper;
using Blogy.Business.DTOs.ContactDtos;
using Blogy.DataAccess.Repositories.ContactRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.ContactServices
{
    public class ContactService(IContactRepository _contactRepository,
                                IMapper _mapper) : IContactService
    {
        public async Task CreateAsync(SendMessageDto createDto)
        {
            var contact = _mapper.Map<Contact>(createDto);

            contact.CreatedDate = DateTime.Now; 
            contact.IsRead = false;         

            await _contactRepository.CreateAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }

        public async Task<List<ResultContactDto>> GetAllAsync()
        {
            var values = await _contactRepository.GetAllAsync();
            return _mapper.Map<List<ResultContactDto>>(values);
        }

        public async Task<UpdateContactDto> GetByIdAsync(int id)
        {
            var value = await _contactRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateContactDto>(value);
        }

        public async Task<ResultContactDto> GetSingleByIdAsync(int id)
        {
            var value = await _contactRepository.GetByIdAsync(id);
            return _mapper.Map<ResultContactDto>(value);
        }

        public async Task UpdateAsync(UpdateContactDto updateDto)
        {
            //var contact = _mapper.Map<Contact>(updateDto);
            //await _contactRepository.UpdateAsync(contact);

            // 1. ADIM: Önce veritabanındaki mevcut, dolu kaydı çekiyoruz (Email, Name vs. gelsin diye)
            var values = await _contactRepository.GetByIdAsync(updateDto.Id);

            // 2. ADIM: Eğer kayıt bulunduysa, sadece değiştirmek istediğimiz alanı güncelliyoruz
            if (values != null)
            {
                values.IsRead = updateDto.IsRead; // Sadece okundu bilgisini değiştir

                // Diğer alanlar (Email, Name vb.) veritabanından geldiği gibi korunur, silinmez.

                // 3. ADIM: Güncellenmiş haliyle tekrar repoya gönderiyoruz
                await _contactRepository.UpdateAsync(values);
            }
        }
    }
}
