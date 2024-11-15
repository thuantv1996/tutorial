using DAL.Models;
using DAL.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _dbContext;

        public CategoryRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _dbContext.Set<Category>().Remove(category);
        }

        public Task<List<Category>> GetAsync()
        {
            var categories = _dbContext.Categories.Where(x => x.IsActive == true).ToListAsync();
            return categories;
        }

        public Task<Category> GetAsync(Guid id)
        {
            var category = _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            return category;
        }
    }
}
