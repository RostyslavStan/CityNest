namespace CityNest
{
    public interface IUsersServices
    {
        public Task Register(string Name, string Email, string Password, long PhoneNumber);
        public Task<string> Login(string password, string email);
        public Task Delete(Guid Id);
        public Task Update(User user);
    }
}
