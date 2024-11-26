using CityNest.Contracts;
using CityNest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CityNest
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
        [HttpPost("Create")]
        [Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request)
        {
            try
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
                return Ok(new { message = "Property created successfully" });
            }
            catch
            {
                return BadRequest("У вас немає доступу до цієї функції.");
            }
        }


        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string? Search)
        {
            // Перевіряємо, чи користувач залогінений
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Ви не залогінені. Будь ласка, увійдіть, щоб здійснити пошук." });
            }

            if (string.IsNullOrEmpty(Search))
            {
                return Ok(new List<Property>());
            }

            try
            {
                var propertiesQuery = dbContext.Properties
                    .AsEnumerable()  // Виконуємо запит на клієнті
                    .Where(p =>
                        p.Title.ToLower().Contains(Search.ToLower()) ||
                        p.Description.ToLower().Contains(Search.ToLower()) ||
                        p.Address.ToLower().Contains(Search.ToLower()) ||
                        p.City.ToLower().Contains(Search.ToLower()) ||
                        p.Region.ToLower().Contains(Search.ToLower()) ||
                        p.PostalCode.ToString().Contains(Search) ||
                        p.Price.ToString().Contains(Search) ||
                        p.Rooms.ToString().Contains(Search) ||
                        p.Square.ToString().Contains(Search) ||
                        p.PropertyType.ToString().Contains(Search) ||
                        p.OfferType.ToString().Contains(Search) ||
                        p.IsForSale.ToString().Contains(Search)
                    )
                    .ToList();  // Завершуємо запит і переводимо результат на клієнтську частину

                return Ok(propertiesQuery);
            }
            catch (Exception ex)
            {
                // Якщо сталася помилка, повертаємо її
                return BadRequest(new { message = "Сталася помилка при виконанні запиту", error = ex.Message });
            }
        }

        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> Filter([FromQuery] string? Search, string? sortItem, string? sortOrder, CancellationToken ct)
        {
            var propertiesQuery = dbContext.Properties
                .Where(p => p.Title.ToLower().Contains(Search.ToLower()));

            if (sortOrder == "desc")
            {
                propertiesQuery = propertiesQuery.OrderByDescending(SelectorKey(sortItem));
            }
            else
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
            switch (sortItem)
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
        [Authorize]
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


        [HttpGet("property")]
        [Authorize]
        public async Task<IActionResult> GetProperty([FromQuery] string title)
        {
            var property = await propertiesService.GetProperty(title);
            return Ok(property);
        }



        [HttpGet("propertiesCard")]
        [Authorize]
        public async Task<IActionResult> GetPropertiesForCard()
        {
            var properties = await propertiesService.GetProperties();

            var responce = properties.Select(n => new PropertyDtoCard(
                n.Title,
                n.City,
                n.Price,
                n.Rooms,
                n.Square,
                n.Images)).ToList();

            return Ok(responce);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> DeleteProperty([FromBody] Guid id)
        {
            await propertiesService.DeleteProperty(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AgentPolicy")]
        public async Task<IActionResult> UpdateProperty(PropertyDto request)
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
            await propertiesService.UpdateProperty(property);
            return Ok();
        }
    }
}
