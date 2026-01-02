using AutoMapper;
using Blogy.Business.DTOs.AddressDtos;
using Blogy.DataAccess.Repositories.AddressRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.AddressServices
{
    public class AddressService(IAddressRepository _AddressRepository,
                               IMapper _mapper) : IAddressService
    {
        public async Task CreateAsync(CreateAddressDto createDto)
        {
            var entity = _mapper.Map<Address>(createDto);
            await _AddressRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _AddressRepository.DeleteAsync(id);
        }

        public async Task<List<ResultAddressDto>> GetAllAsync()
        {
            var values = await _AddressRepository.GetAllAsync();
            return _mapper.Map<List<ResultAddressDto>>(values);
        }

        public async Task<UpdateAddressDto> GetByIdAsync(int id)
        {
            var value = await _AddressRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateAddressDto>(value);
        }

        public async Task<ResultAddressDto> GetSingleByIdAsync(int id)
        {
            var value = await _AddressRepository.GetByIdAsync(id);
            return _mapper.Map<ResultAddressDto>(value);
        }

        public async Task UpdateAsync(UpdateAddressDto updateDto)
        {
            var entity = _mapper.Map<Address>(updateDto);
            await _AddressRepository.UpdateAsync(entity);
        }
    }
}
