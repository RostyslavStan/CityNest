using Microsoft.AspNetCore.Mvc;

namespace CityNest
{
    public class CreatePropertyRequest
    {
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int rooms { get; set; }
        
        [FromForm]  
        public List<IFormFile> images { get; set; } = new List<IFormFile>();
    }

}
