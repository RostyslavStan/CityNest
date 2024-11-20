using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace CityNest
{
    public class UsersRepository(RealStateDbContext dbContext): IUsersRepository
    {
        public async Task<List<User>> Get()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<User> GetByName(string name)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }
        public async Task<User> GetByPhoneNumber(long phoneNumber)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            return user;
        }

        public async Task Add(User user)
        {
            var roleEntity = await dbContext.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Agent)
                ?? throw new InvalidOperationException();

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            await dbContext.Users
            .Where(c => c.Id == user.Id)
            .ExecuteUpdateAsync(s => s
            .SetProperty(c => c.Name, user.Name)
            .SetProperty(c => c.Email, user.Email)
            .SetProperty(c => c.PasswordHash, user.PasswordHash)
            .SetProperty(c => c.PhoneNumber, user.PhoneNumber));
        }

        public async Task Delete(Guid id)
        {
            await dbContext.Users
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await dbContext.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
    }
}