using CityNest.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CityNest
{
    [ApiController]
    [Authorize]
    [Route("Properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertiesServices propertiesService;
        public PropertiesController(IPropertiesServices propertiesService)
        {
            this.propertiesService = propertiesService;
        }

        [HttpGet("properties")]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await propertiesService.Get();

            return Ok(properties);
        }

        [HttpGet("myProperties")]
        public async Task<IActionResult> GetMyProperties()
        {
            var userId = User.GetUserId();
            var properties = await propertiesService.GetMyProperties(userId);
            return Ok(properties);
        }

        [HttpGet("property")]
        public async Task<IActionResult> GetProperty([FromQuery] Guid id)
        {
            var property = await propertiesService.GetProperty(id);
            return Ok(property);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]  
        public async Task<IActionResult> CreateProperty([FromForm] CreatePropertyRequest request)
        {
            var userId = User.GetUserId();

            await propertiesService.AddProperty(request, userId);
            return Ok(new { message = "Property created successfully" });
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            await propertiesService.DeleteProperty(id);
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProperty(Guid Id,[FromForm] CreatePropertyRequest request)
        {
            var updatedProperty = await propertiesService.UpdateProperty(Id, request);
            return Ok(updatedProperty);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string Search)
        {
            var propertiesQuery = await propertiesService.SearchProperties(Search);
                return Ok(propertiesQuery);
        }

        [HttpGet("filter")]
        public async Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null)
        {
            return await propertiesService.FilterProperties(
                minPrice,
                maxPrice,
                rooms);
        }
    }
}
