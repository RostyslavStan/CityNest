namespace CityNest
{
    public record UsersRegisterRequest(
        string name,
        string email, 
        string passwordHash, 
        string phoneNumber);
}
