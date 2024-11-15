using DAL.Models;

namespace DAL.Repositories.Abstracts
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Delete(Category category);
        Task<List<Category>> GetAsync();
        Task<Category> GetAsync(Guid id);
    }
}
