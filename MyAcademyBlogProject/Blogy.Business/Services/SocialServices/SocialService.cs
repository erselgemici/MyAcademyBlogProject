using AutoMapper;
using Blogy.Business.DTOs.SocialDtos;
using Blogy.DataAccess.Repositories.SocialRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.SocialServices
{
    public class SocialService(ISocialRepository _socialRepository,
                                IMapper _mapper) : ISocialService
    {
        public async Task CreateAsync(CreateSocialDto createDto)
        {
            var entity = _mapper.Map<Social>(createDto);
            await _socialRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _socialRepository.DeleteAsync(id);
        }

        public async Task<List<ResultSocialDto>> GetAllAsync()
        {
            var values = await _socialRepository.GetAllAsync();
            return _mapper.Map<List<ResultSocialDto>>(values);
        }

        public async Task<UpdateSocialDto> GetByIdAsync(int id)
        {
            var value = await _socialRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateSocialDto>(value);
        }

        public async Task<ResultSocialDto> GetSingleByIdAsync(int id)
        {
            var value = await _socialRepository.GetByIdAsync(id);
            return _mapper.Map<ResultSocialDto>(value);
        }

        public async Task UpdateAsync(UpdateSocialDto updateDto)
        {
            var entity = _mapper.Map<Social>(updateDto);
            await _socialRepository.UpdateAsync(entity);
        }
    }
}
