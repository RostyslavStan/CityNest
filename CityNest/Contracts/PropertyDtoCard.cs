namespace CityNest
{
    public record PropertyDtoCard(
        string title,
        string city,
        decimal price,
        int rooms,
        int square,
        List<string> images);
}
