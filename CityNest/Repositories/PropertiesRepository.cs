using Microsoft.EntityFrameworkCore;

namespace CityNest
{
    public class PropertiesRepository(RealStateDbContext dbContext) : IPropertiesRepository
    {
        public async Task<List<Property>> Get()
        {
            return await dbContext.Properties.AsNoTracking().ToListAsync();
        }
        public async Task<Property> GetProperty(string title)
        {
            var property = await dbContext.Properties
             .Where(p => p.Title == title)  // фільтруємо за id
                .FirstOrDefaultAsync();
            return property;
        }
        public async Task<List<PropertyDtoCard>> GetFilter()
        {
            // Вибір лише потрібних полів з бази даних
            return await dbContext.Properties
                .AsNoTracking()  // Для покращення продуктивності
                .Select(n => new PropertyDtoCard(
                    n.Title,        // Назва
                    n.City,         // Місто
                    n.Price,        // Ціна
                    n.Rooms,        // Кількість кімнат
                    n.Square,       // Площа
                    n.Images        // Список зображень
                ))
                .ToListAsync();
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
                    .SetProperty(p => p.Address, property.Address)
                    .SetProperty(p => p.City, property.City)
                    .SetProperty(p => p.Region, property.Region)
                    .SetProperty(p => p.PostalCode, property.PostalCode)
                    .SetProperty(p => p.Price, property.Price)
                    .SetProperty(p => p.Rooms, property.Rooms)
                    .SetProperty(p => p.Square, property.Square)
                    .SetProperty(p => p.PropertyType, property.PropertyType)
                    .SetProperty(p => p.IsForSale, property.IsForSale)
                    .SetProperty(p => p.AgentsId, property.AgentsId)
                    .SetProperty(p => p.Images, property.Images));
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await dbContext.Properties.Where(c => c.Id == id)
            .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }
    }
}