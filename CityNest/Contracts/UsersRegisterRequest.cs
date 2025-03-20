namespace CityNest
{
    public record UsersRegisterRequest(
        string Name,
        string Email, 
        string PasswordHash, 
        long PhoneNumber);
}
