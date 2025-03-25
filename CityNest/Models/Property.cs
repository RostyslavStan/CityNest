using System.Text.Json.Serialization;

namespace CityNest
{
    public class Property
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public List<string> Images { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public Property(
            string title,
            string description,
            string city,
            decimal price,
            int rooms,
            List<string> images)
        {
            Title = title; 
            Description = description; 
            City = city;
            Price = price;
            Rooms = rooms;
            Images = images;
        }
        [JsonConstructor]
        public Property(
            string title,
            string description,
            string city,
            decimal price,
            int rooms,
            List<string> images,
            Guid userId)
        {
            Title = title;
            Description = description;
            City = city;
            Price = price;
            Rooms = rooms;
            Images = images;
            UserId = userId;
        }
    }
}
