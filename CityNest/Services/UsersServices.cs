using CityNest.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CityNest
{
    public class UsersServices: IUsersServices
    {
        private readonly IUsersRepository usersRepository;
        private readonly IAgentsRepository agentsRepository;
        private readonly IJwtProvider jwtProvider;

        public UsersServices(IUsersRepository usersRepository, IAgentsRepository agentsRepository, IJwtProvider jwtProvider)
        {
            this.usersRepository = usersRepository;
            this.jwtProvider = jwtProvider;
            this.agentsRepository = agentsRepository;
        }
        public async Task Register(string Name, string Email, string Password, long PhoneNumber)
        {
            var hashedPassword = PasswordHasher.Generate(Password);

            if (Email.EndsWith("@lnu.edu.ua"))
            {
                var agent = new Agent(Guid.NewGuid(), Name, Email, hashedPassword, PhoneNumber);
                await agentsRepository.Add(agent);
            }
            else
            {
                var user = new User(Guid.NewGuid(), Name, Email, hashedPassword, PhoneNumber);

                await usersRepository.Add(user);
            }
        }

        public async Task<string> Login(string password, string email)
        {
            
            if (email.EndsWith("@lnu.edu.ua"))
            {
                var agent = await agentsRepository.GetByEmail(email);

                var result = PasswordHasher.Verify(password, agent.PasswordHash);

                if (result == false)
                {
                    throw new Exception("Failed to login");
                }

                var token = jwtProvider.GenerateTokenForAgent(agent);
                return token;
            }
            else
            {
                var user = await usersRepository.GetByEmail(email);

                var result = PasswordHasher.Verify(password, user.PasswordHash);

                if (result == false)
                {
                    throw new Exception("Failed to login");
                }

                var token = jwtProvider.GenerateTokenForUser(user);
                return token;
            }
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