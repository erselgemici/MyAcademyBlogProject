using Blogy.DataAccess.Context;
using Blogy.DataAccess.Repositories.GenericRepositories;
using Blogy.Entity.Entities;
using Blogy.Entity.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogy.DataAccess.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategoriesWithBlogAsync()
        {
            return await _context.Categories.AsNoTracking().Include(x=>x.Blogs).ToListAsync();
        }

        public List<CategoryBlogCount> GetCategoriesWithCount()
        {
            var values = _context.Categories.Select(x=> new CategoryBlogCount
            {
                Id = x.Id,
                CategoryName = x.Name,
                Count = x.Blogs.Count
            }).ToList();

            return values;
        }
    }
}
