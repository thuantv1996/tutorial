using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Models;
using NewsApi.ViewModels;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Get all news
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            await Task.Delay(50);
            var result = NewsDbContext.News;
            return Ok(result);
        }

        /// <summary>
        /// Create a news
        /// </summary>
        /// <param name="news">news data</param>
        /// <returns>201 status code with url and object created</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] News news)
        {
            try
            {
                news.Id = Guid.NewGuid();
                NewsDbContext.News.Add(news);
                await Task.Delay(50);
                return Created($"api/news/{news.Id}", news);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a news
        /// </summary>
        /// <param name="id">id of news that you need to update</param>
        /// <param name="news">Object update</param>
        /// <returns>204 if update success</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] News news)
        {
            try
            {
                var newsEntity = NewsDbContext.News.FirstOrDefault(x => x.Id == id);
                if (newsEntity == null)
                {
                    return NotFound($"cannot find news with id: {id}");
                }
                newsEntity.Title = news.Title;
                newsEntity.Content = news.Content;
                await Task.Delay(50);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// allow to change any property od news
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody]JsonPatchDocument<News> patchDoc)
        {
            try
            {
                var newsEntity = NewsDbContext.News.FirstOrDefault(x => x.Id == id);
                if (newsEntity == null)
                {
                    return NotFound($"cannot find news with id: {id}");
                }

                patchDoc.ApplyTo(newsEntity, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await Task.Delay(50);
                return Ok(newsEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    


        /// <summary>
        /// Delete a news
        /// </summary>
        /// <param name="id">id of news that you need to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var newsEntity = NewsDbContext.News.FirstOrDefault(x => x.Id == id);
                if (newsEntity == null)
                {
                    return NotFound($"cannot find news with id: {id}");
                }
                NewsDbContext.News.Remove(newsEntity);
                await Task.Delay(50);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("uploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImgaeAsync([FromForm] UploadImage data)
        {
            var newsEntity = NewsDbContext.News.FirstOrDefault(x => x.Id == data.NewsId);
            if (newsEntity == null)
            {
                return NotFound($"cannot find news with id: {data.NewsId}");
            }

            if(data.File.Length == 0)
            {
                return BadRequest("File empty");
            }

            string path = $"{_webHostEnvironment.WebRootPath}/images/{data.NewsId}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath = $"{path}/{data.File.FileName}";
            using(FileStream stream = new FileStream(filePath, new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.ReadWrite }))
            {
                await data.File.CopyToAsync(stream);
            }

            newsEntity.ImageLink = $"images/{data.NewsId}/{data.File.FileName}";

            return Ok();
        }
    }
}
