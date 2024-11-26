namespace CityNest
{
    public interface IPropertiesRepository
    {
        public Task<List<Property>> Get();
        public Task<Property> GetProperty(string title);
        public Task<List<PropertyDtoCard>> GetFilter();
        public Task Add(Property property);
        public Task Update(Property property);
        public Task Delete(Guid id);
    }
}
