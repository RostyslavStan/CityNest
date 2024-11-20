using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CityNest
{
    public class UsersServices: IUsersServices
    {
        private readonly IUsersRepository usersRepository;
        private readonly IJwtProvider jwtProvider;

        public UsersServices(IUsersRepository usersRepository, IJwtProvider jwtProvider)
        {
            this.usersRepository = usersRepository;
            this.jwtProvider = jwtProvider;
        }
        public async Task Register(string Name, string Email, string Password, long PhoneNumber)
        {
            var hashedPassword = PasswordHasher.Generate(Password);

            var user = new User(Guid.NewGuid(), Name, Email, hashedPassword, PhoneNumber);

            await usersRepository.Add(user);
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

        public async Task Update(User user)
        {
            await usersRepository.Update(user);
        }

        public async Task Delete(Guid Id)
        {
            await usersRepository.Delete(Id);
        }

    }
}