using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CityNest
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository usersRepository;
        private readonly IJwtProvider jwtProvider;
        public UsersServices(
            IUsersRepository usersRepository,
            IJwtProvider jwtProvider)
        {
            this.usersRepository = usersRepository;
            this.jwtProvider = jwtProvider;
        }

        public async Task<User> Register(string Name, string Email, string Password, string PhoneNumber)
        {
            var hashedPassword = PasswordHasher.Generate(Password);
            var user = new User(Name, Email, hashedPassword, PhoneNumber);

            await usersRepository.Add(user);
            return user;
        }

        public async Task<string> Login(string password, string email)
        {
            var user = await usersRepository.GetByEmail(email);

            var result = PasswordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = jwtProvider.GenerateToken(user);
            return token;
        }
        public async Task Update(UsersRegisterRequest user)
        {
            await usersRepository.Update(user);
        }

        public async Task Delete(Guid id)
        {

            await usersRepository.Delete(id);
        }
    }
}
