namespace CityNest
{
    public interface IPropertiesServices
    {
        public Task AddProperty(CreatePropertyRequest request, Guid userId);
        public Task DeleteProperty(Guid Id);
        public Task<List<Property>> Get();
        public Task<List<Property>> GetMyProperties(Guid userId);
        public Task<Property> GetProperty(Guid id);
        public Task<List<Property>> SearchProperties(string searchString);
        public Task<Property> UpdateProperty(Guid Id, CreatePropertyRequest request);
        public Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null);
    }
}
