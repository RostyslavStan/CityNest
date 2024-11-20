using CityNest.Contracts;
using CityNest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CityNest.Controllers
{
    [ApiController]
    [Route("Properties")]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly RealStateDbContext dbContext;
        private readonly IPropertiesServices propertiesService;
        public PropertiesController(RealStateDbContext dbContext, IPropertiesServices propertiesService)
        {
            this.dbContext = dbContext;
            this.propertiesService = propertiesService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request, CancellationToken ct)
        {
            var property = new Property(
                request.title,
                request.description,
                request.address,
                request.city,
                request.region,
                request.postalCode,
                request.price,
                request.rooms,
                request.square,
                request.propertyType,
                request.offerType,
                request.isForSale,
                request.images,
                request.agentsId,
                request.dateUpdated);

            await propertiesService.CreateProperty(property);
            return Ok();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string? Search, string? sortItem, string? sortOrder, CancellationToken ct)
        {
            var propertiesQuery = dbContext.Properties
                .Where(p => p.Title.ToLower().Contains(Search.ToLower()));

            if(sortOrder == "desc")
            {
                propertiesQuery = propertiesQuery.OrderByDescending(SelectorKey(sortItem));
            } else
            {
                propertiesQuery = propertiesQuery.OrderBy(SelectorKey(sortItem));

            }

            var properties = await propertiesQuery.Select(n => new PropertyDto(
                n.Id,
                n.Title,
                n.Description,
                n.Address,
                n.City,
                n.Region,
                n.PostalCode,
                n.Price,
                n.Rooms,
                n.Square,
                n.PropertyType,
                n.OfferType,
                n.IsForSale,
                n.Images,
                n.AgentsId,
                n.DateUpdated)).ToListAsync(ct);
                return Ok(new GetPropertiesResponce(properties));
        }

        private Expression<Func<Property, object>> SelectorKey(string sortItem)
        {
            switch(sortItem)
            {
                case "date":
                    return property => property.DateUpdated;
                case "city":
                    return property => property.City;
                case "region":
                    return property => property.Region;
                case "rooms":
                    return property => property.Rooms;
                case "square":
                    return property => property.Square;
                case "price":
                    return property => property.Price;
                case "propertytype":
                    return property => property.PropertyType;
                case "offertype":
                    return property => property.OfferType;
                case "agent":
                    return property => property.AgentsId;
                default:
                    return property => property.Title;
            }
        }

        [HttpGet("properties")]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await propertiesService.GetProperties();

            var responce = properties.Select(n => new PropertyDto(
                n.Id,
                n.Title,
                n.Description,
                n.Address,
                n.City,
                n.Region,
                n.PostalCode,
                n.Price,
                n.Rooms,
                n.Square,
                n.PropertyType,
                n.OfferType,
                n.IsForSale,
                n.Images,
                n.AgentsId,
                n.DateUpdated)).ToList();

            return Ok(responce);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProperty([FromBody] Guid id)
        {
            await propertiesService.DeleteProperty(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromBody] Property property)
        {
            await propertiesService.UpdateProperty(property);
            return Ok();
        }
    }
}
