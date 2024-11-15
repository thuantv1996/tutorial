using AutoMapper;
using BLL.Abstracts;
using DAL;
using DAL.Models;
using DAL.Repositories.Abstracts;
using DTO;

namespace BLL.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult> AddAsync(CategoryDTO categoryDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                category.Id = Guid.NewGuid();
                category.CreatedDate = DateTime.UtcNow;
                category.IsActive = true;
                
                _categoryRepository.Add(category);
                await _unitOfWork.SaveChangesAsync();

                categoryDto.Id = category.Id;
                return new ApiResult
                {
                    StatusCode = 201,
                    ResponseData = categoryDto
                };
            }
            catch(Exception e)
            {
                return new ApiResult
                {
                    StatusCode = 500,
                    Error = e
                };
            }
        }

        public async Task<ApiResult> DeleteAsync(Guid id)
        {
            try
            {
                Category category = await _categoryRepository.GetAsync(id);

                if (category == null)
                {
                    return NotFoundResource(id);
                }

                _categoryRepository.Delete(category);
            
                await _unitOfWork.SaveChangesAsync();

                return new ApiResult
                {
                    StatusCode = 204,
                };
            }
            catch(Exception e)
            {
                return new ApiResult
                {
                    StatusCode = 500,
                    Error = e
                };
            }
        }

        public async Task<ApiResult> GetAsync()
        {
            List<Category> categories = await _categoryRepository.GetAsync();
            List<CategoryDTO> categoryDTOs = _mapper.Map< List<CategoryDTO>>(categories);
            return new ApiResult
            {
                StatusCode = 200,
                ResponseData = categoryDTOs
            };
        }

        public async Task<ApiResult> GetAsync(Guid id)
        {
            Category category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFoundResource(id);
            }
            
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return new ApiResult
            {
                StatusCode = 200,
                ResponseData = categoryDTO
            };
        }

        private static ApiResult NotFoundResource(Guid id)
        {
            return new ApiResult
            {
                StatusCode = 404,
                Error = $"Cannot find category with id: {id}"
            };
        }
    }
}
