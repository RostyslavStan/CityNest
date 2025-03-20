namespace CityNest
{
    public interface IPropertiesRepository
    {
        public Task<List<Property>> Get();
        public Task<Property> GetProperty(Guid id);
        public Task<List<Property>> SearchProperties(string searchString);
        public Task Add(Property property);
        public Task Update(Property property);
        public Task Delete(Guid id);
        public Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null);
    }
}
