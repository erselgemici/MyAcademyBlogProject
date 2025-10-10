using Blogy.Business.DTOs.BlogDtos;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.BlogServices
{
    public interface IBlogService : IGenericService<ResultBlogDto, UpdateBlogDto, CreateBlogDto>
    {
        Task<List<ResultBlogDto>> GetBlogsWithCategoriesAsync();
    }
}
