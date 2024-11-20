namespace CityNest
{
    public interface IPropertiesRepository
    {
        public Task<List<Property>> Get();
        public Task Add(Property property);
        public Task Update(Property property);
        public Task Delete(Guid id);
    }
}
