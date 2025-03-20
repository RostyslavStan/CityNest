using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CityNest
{
    public class PropertiesRepository(RealStateDbContext dbContext) : IPropertiesRepository
    {
        public async Task<List<Property>> Get()
        {
            return await dbContext.Properties.AsNoTracking().ToListAsync();
        }

        public async Task<Property> GetProperty(Guid id)
        {
            var property = await dbContext.Properties
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return property;
        }
        public async Task<List<Property>> SearchProperties(string searchString)
        {
            var searchWords = searchString
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLower())
                .ToList();

            var properties = dbContext.Properties.AsNoTracking();

            foreach (var word in searchWords)
            {
                properties = properties.Where(p =>
                    p.Title.ToLower().Contains(word) ||
                    p.Description.ToLower().Contains(word) ||
                    p.City.ToLower().Contains(word));
            }

            return properties.AsEnumerable().ToList();
        }


        public async Task Add(Property property)
        {
            await dbContext.AddAsync(property);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Property property)
        {
            await dbContext.Properties
                .Where(p => p.Id == property.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.Title, property.Title)
                    .SetProperty(p => p.Description, property.Description)
                    .SetProperty(p => p.City, property.City)
                    .SetProperty(p => p.Price, property.Price)
                    .SetProperty(p => p.Rooms, property.Rooms)
                    .SetProperty(p => p.Images, property.Images));
            await dbContext.SaveChangesAsync();

        }

        public async Task Delete(Guid id)
        {
            await dbContext.Properties.Where(c => c.Id == id)
            .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }
        public async Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null)
        {
            var query = dbContext.Properties.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (rooms.HasValue)
            {
                query = query.Where(p => p.Rooms == rooms.Value);
            }

            return await query.ToListAsync();
        }


    }
}