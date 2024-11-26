namespace CityNest
{
    public class Property
    {

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int PostalCode { get; set; }
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Square { get; set; }
        public PropertyType PropertyType{ get; set; }
        public OfferType OfferType { get; set; }
        public bool IsForSale { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public DateTime? DateUpdated { get; set; }
        public Guid AgentsId { get; set; }
        public Agent? Agent { get; set; }

        public Property(string title,
            string description,
            string address,
            string city,
            string region,
            int postalCode,
            decimal price,
            int rooms,
            int square,
            PropertyType propertyType,
            OfferType offerType,
            bool isForSale,
            List<string> images,
            Guid agentsId,
            DateTime? dateUpdated = null )
        {
            Title = title; 
            Description = description; 
            Address = address; 
            City = city;
            Region = region;
            PostalCode = postalCode;
            Price = price;
            Rooms = rooms;
            Square = square;
            PropertyType = propertyType;    
            OfferType = offerType;
            IsForSale = isForSale;
            Images = images;
            AgentsId = agentsId;
            DateUpdated = dateUpdated;
        }
    }
}
