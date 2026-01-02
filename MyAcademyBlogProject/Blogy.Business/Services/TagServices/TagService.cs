using AutoMapper;
using Blogy.Business.DTOs.TagDtos;
using Blogy.DataAccess.Repositories.TagRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.TagServices
{

    public class TagService(ITagRepository _tagRepository,
                            IMapper _mapper) : ITagService
    {
        public async Task CreateAsync(CreateTagDto createDto)
        {
            var entity = _mapper.Map<Tag>(createDto);
            await _tagRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _tagRepository.DeleteAsync(id);
        }

        public async Task<List<ResultTagDto>> GetAllAsync()
        {
            var values = await _tagRepository.GetAllAsync();
            return _mapper.Map<List<ResultTagDto>>(values);
        }

        public async Task<UpdateTagDto> GetByIdAsync(int id)
        {
            var value = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateTagDto>(value);
        }

        public async Task<ResultTagDto> GetSingleByIdAsync(int id)
        {
            var value = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<ResultTagDto>(value);
        }

        public async Task UpdateAsync(UpdateTagDto updateDto)
        {
            var entity = _mapper.Map<Tag>(updateDto);
            await _tagRepository.UpdateAsync(entity);
        }
    }
}

