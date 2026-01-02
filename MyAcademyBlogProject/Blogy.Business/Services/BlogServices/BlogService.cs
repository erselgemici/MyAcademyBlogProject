using AutoMapper;
using Blogy.Business.DTOs.BlogDtos;
using Blogy.DataAccess.Repositories.BlogRepositories;
using Blogy.Entity.Entities;
using System.Reflection.Metadata;

namespace Blogy.Business.Services.BlogServices
{
    public class BlogService(IBlogRepository _blogRepository, IMapper _mapper) : IBlogService
    {
        public async Task CreateAsync(CreateBlogDto createDto)
        {
            var entity = _mapper.Map<Blog>(createDto);
            entity.CreatedDate = DateTime.Now;
            await _blogRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _blogRepository.DeleteAsync(id);
        }

        public async Task<List<ResultBlogDto>> GetAllAsync()
        {
            var values = await _blogRepository.GetAllAsync();
            return _mapper.Map<List<ResultBlogDto>>(values);
        }

        public async Task<List<ResultBlogDto>> GetBlogsByCategoryIdAsync(int categoryId)
        {
            var values = await _blogRepository.GetAllAsync(x => x.CategoryId == categoryId);
            return _mapper.Map<List<ResultBlogDto>>(values);
        }

        public async Task<List<ResultBlogDto>> GetBlogsWithCategoriesAsync()
        {
            var values = await _blogRepository.GetBlogsWithCategoriesAsync();
            return _mapper.Map<List<ResultBlogDto>>(values);
        }

        public async Task<UpdateBlogDto> GetByIdAsync(int id)
        {
            var value = await _blogRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateBlogDto>(value);
        }

        public async Task<List<ResultBlogDto>> GetLastNBlogsAsync(int count)
        {
            var values = await _blogRepository.GetLastNBlogsAsync(count);
            return _mapper.Map<List<ResultBlogDto>>(values);
        }     

        public async Task<ResultBlogDto> GetSingleByIdAsync(int id)
        {
            var value = await _blogRepository.GetByIdAsync(id);
            return _mapper.Map<ResultBlogDto>(value);
        }

        public async Task UpdateAsync(UpdateBlogDto updateDto)
        {
            var entity = _mapper.Map<Blog>(updateDto);
            await _blogRepository.UpdateAsync(entity);
        }
    }
}
