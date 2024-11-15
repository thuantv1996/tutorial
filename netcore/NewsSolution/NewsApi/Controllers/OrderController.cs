using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Models;
using System.Linq;
using AutoMapper;
using NewsApi.ViewModels;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        public OrderController(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrdersAsync()
        {
            try
            {
                var orders = NewsDbContext.Order;
                var resp = _autoMapper.Map<List<OrderVM>>(orders);
                var resp2 = _autoMapper.Map<List<Order>>(resp);
                return Ok(resp2);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
