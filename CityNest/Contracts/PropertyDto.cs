namespace CityNest
{
    public record PropertyDto(
        Guid Id,
        string title,
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
        DateTime? dateUpdated = null);
}
