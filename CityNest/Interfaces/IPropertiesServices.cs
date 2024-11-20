namespace CityNest.Interfaces
{
    public interface IPropertiesServices
    {
        Task CreateProperty(Property property);
        Task DeleteProperty(Guid Id);
        Task<List<Property>> GetProperties();
        Task UpdateProperty(Property property);
    }
}