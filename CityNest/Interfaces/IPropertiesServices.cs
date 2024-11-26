namespace CityNest.Interfaces
{
    public interface IPropertiesServices
    {
        Task CreateProperty(Property property);
        Task<Property> GetProperty(string title);

        Task DeleteProperty(Guid Id);
        Task<List<Property>> GetProperties();
        Task UpdateProperty(Property property);
    }
}