using AutoMapper;
using Blogy.Business.DTOs.AboutDtos;
using Blogy.DataAccess.Repositories.AboutRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.AboutServices
{
    public class AboutService(IAboutRepository _aboutRepository,
                                IMapper _mapper) : IAboutService
    {
        public async Task CreateAsync(CreateAboutDto createDto)
        {
            var entity = _mapper.Map<About>(createDto);
            await _aboutRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _aboutRepository.DeleteAsync(id);
        }

        public async Task<List<ResultAboutDto>> GetAllAsync()
        {
            var values = await _aboutRepository.GetAllAsync();
            return _mapper.Map<List<ResultAboutDto>>(values);
        }

        public async Task<UpdateAboutDto> GetByIdAsync(int id)
        {
            var value = await _aboutRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateAboutDto>(value);
        }

        public async Task<ResultAboutDto> GetSingleByIdAsync(int id)
        {
            var value = await _aboutRepository.GetByIdAsync(id);
            return _mapper.Map<ResultAboutDto>(value);
        }

        public async Task UpdateAsync(UpdateAboutDto updateDto)
        {        
            var entity = await _aboutRepository.GetByIdAsync(updateDto.Id);         
            _mapper.Map(updateDto, entity);
            await _aboutRepository.UpdateAsync(entity);
        }
    }
}
