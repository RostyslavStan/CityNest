namespace CityNest
{
    public class PropertiesDtoCard
    {
        public string Title { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public double Square { get; set; }
        public List<string> Images { get; set; }

        // Конструктор для ініціалізації
        public PropertiesDtoCard(string title, string city, decimal price, int rooms, double square, List<string> images)
        {
            Title = title;
            City = city;
            Price = price;
            Rooms = rooms;
            Square = square;
            Images = images;
        }
    }

}
