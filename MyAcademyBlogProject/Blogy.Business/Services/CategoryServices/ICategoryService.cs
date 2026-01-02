using Blogy.Business.DTOs.CategoryDtos;

namespace Blogy.Business.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task<List<ResultCategoryDto>> GetCategoriesWithBlogAsync();
        Task<UpdateCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
        List<ResultCategoryDto> GetCategoriesWithCount();
    }
}
