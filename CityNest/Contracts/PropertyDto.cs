namespace CityNest
{
    public class PropertyDto
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string City { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public List<string> Images { get; set; }

        public PropertyDto(string title, string description,  string city, decimal price, int rooms, List<string> images)
        {
            Title = title;
            Description = description;
            City = city;
            Price = price;
            Rooms = rooms;
            Images = images;
        }
    }

}
