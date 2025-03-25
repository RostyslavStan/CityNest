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
        public PropertyDto(string Title, string Description, string City, decimal Price, int Rooms, List<string> images)
        {
            this.Title = Title;
            this.Description = Description;
            this.City = City;
            this.Price = Price;
            this.Rooms = Rooms;
            this.Images = images;
        }
    }

}
