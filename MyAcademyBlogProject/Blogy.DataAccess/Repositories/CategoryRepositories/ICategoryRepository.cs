using Blogy.DataAccess.Repositories.GenericRepositories;
using Blogy.Entity.Entities;
using Blogy.Entity.Entities.Models;

namespace Blogy.DataAccess.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithBlogAsync();
        List<CategoryBlogCount> GetCategoriesWithCount();
    }
}
