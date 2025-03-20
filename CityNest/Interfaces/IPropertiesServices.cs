namespace CityNest
{
    public interface IPropertiesServices
    {
        Task AddProperty(CreatePropertyRequest request, Guid userId);
        Task DeleteProperty(Guid Id);
        Task<List<Property>> Get();
        Task<Property> GetProperty(Guid id);
        Task<List<Property>> SearchProperties(string searchString);
        Task UpdateProperty(Property property);
        public Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null);
    }
}
