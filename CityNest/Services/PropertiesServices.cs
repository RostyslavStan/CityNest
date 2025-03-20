using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityNest
{
    public class PropertiesServices : IPropertiesServices
    {
        private readonly IPropertiesRepository propertiesRepository;
        public PropertiesServices(IPropertiesRepository propertiesRepository)
        {
            this.propertiesRepository = propertiesRepository;
        }

        public async Task<List<Property>> Get()
        {
            return await propertiesRepository.Get();
        }

        public async Task<Property> GetProperty(Guid id)
        {
            return await propertiesRepository.GetProperty(id);
        }
        public async Task<List<Property>> SearchProperties(string searchString)
        {
            return await propertiesRepository.SearchProperties(searchString);
        }

        public async Task<List<string>> SaveImages(CreatePropertyRequest request)
        {
            var imageUrls = new List<string>();

            if (request.images != null && request.images.Count > 0)
            {
                foreach (var file in request.images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine("wwwroot", "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imageUrls.Add($"/uploads/{fileName}");
                }
            }
            return imageUrls;
        }

        public async Task AddProperty([FromForm] CreatePropertyRequest request, Guid userId)
        {
            var imageUrls = await SaveImages(request);
            var property = new Property(
                request.title,
                request.description,
                request.city,
                request.price,
                request.rooms,
                imageUrls,
                userId);

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

        public async Task<List<Property>> FilterProperties(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? rooms = null)
        {
            return await propertiesRepository.FilterProperties(
                minPrice,
                maxPrice,
                rooms);
        }
    }
}
