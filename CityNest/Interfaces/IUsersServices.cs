namespace CityNest
{
    public interface IUsersServices
    {
        public Task<User> Register(string Name, string Email, string Password, long PhoneNumber);
        public Task<string> Login(string password, string email);
        public Task Delete(Guid id);
        public Task Update(UsersRegisterRequest user);
    }
}
