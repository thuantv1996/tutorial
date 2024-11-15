using BLL.Abstracts;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;

        public CategoryController(ICategoryService categoryService, IMemoryCache memoryCache)
        {
            _categoryService = categoryService;
            _memoryCache = memoryCache;
        }

        private static void PostEvictionCallback(object cacheKey, object cacheValue, EvictionReason evictionReason, object state)
        {
            System.Console.WriteLine($"{cacheKey} delete by {evictionReason}");
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [EnableRateLimiting("ip")]
        [OutputCache(PolicyName = "BaseCache")]
        public async Task<IActionResult> GetAsync()
        {
            //if(!_memoryCache.TryGetValue("GetAll", out object? value))
            //{
            //    var result = await _categoryService.GetAsync();
            //    value = result.ResponseData;
            //    await Task.Delay(800);
            //    _memoryCache.Set("GetAll", value, new MemoryCacheEntryOptions
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(5),
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20),
            //        Size = 1,
            //    }.RegisterPostEvictionCallback(PostEvictionCallback));
            //}

            var result = await _categoryService.GetAsync();
            Console.WriteLine("Ghi");
            var value = result.ResponseData;
            await Task.Delay(800);
            return Ok(value);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _categoryService.GetAsync(id);
            if(result.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(result.ResponseData);
            }
            return StatusCode(result.StatusCode, result.Error);
        }

        /// <summary>
        /// Crate a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] CategoryDTO category)
        {
            var result = await _categoryService.AddAsync(category);
            if (result.StatusCode == StatusCodes.Status201Created)
            {
                var response = (CategoryDTO)result.ResponseData;
                return Created($"/api/category/{response.Id}", response);
            }
            return StatusCode(result.StatusCode, result.Error);
        }

        /// <summary>
        /// Delete an exists category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            ApiResult result = await _categoryService.DeleteAsync(id);

            if (result.StatusCode == StatusCodes.Status204NoContent)
            {
                return NoContent();
            }

            return StatusCode(result.StatusCode, result.Error);
        }
    }
}
