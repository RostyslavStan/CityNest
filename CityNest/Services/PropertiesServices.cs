using CityNest.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityNest
{
    public class PropertiesServices : IPropertiesServices
    {
        private readonly IPropertiesRepository propertiesRepository;
        public PropertiesServices(IPropertiesRepository propertiesRepository)
        {
            this.propertiesRepository = propertiesRepository;
        }

        public async Task<List<Property>> GetProperties()
        {
            return await propertiesRepository.Get();
        }

        public async Task CreateProperty(Property property)
        {
            await propertiesRepository.Add(property);
        }

        public async Task UpdateProperty(Property property)
        {
            await propertiesRepository.Update(property);
        }

        public async Task DeleteProperty(Guid Id)
        {
            await propertiesRepository.Delete(Id);
        }
    }
}
