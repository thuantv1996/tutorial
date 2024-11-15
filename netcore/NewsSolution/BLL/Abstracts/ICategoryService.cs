using DTO;

namespace BLL.Abstracts
{
    public interface ICategoryService
    {
        Task<ApiResult> AddAsync(CategoryDTO category);
        Task<ApiResult> DeleteAsync(Guid id);
        Task<ApiResult> GetAsync();
        Task<ApiResult> GetAsync(Guid id);
    }
}
