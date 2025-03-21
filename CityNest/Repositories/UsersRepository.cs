﻿using Microsoft.EntityFrameworkCore;


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

        public async Task Add(User user)
        {
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(UsersRegisterRequest user)
        {
            await dbContext.Users
            .Where(c => c.Email == user.Email)
            .ExecuteUpdateAsync(s => s
            .SetProperty(c => c.Name, user.Name)
            .SetProperty(c => c.PasswordHash, user.PasswordHash)
            .SetProperty(c => c.PhoneNumber, user.PhoneNumber));
            PasswordHasher.Generate(user.PasswordHash);
        }

        public async Task Delete(Guid id)
        {
            await dbContext.Users
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }
    }
}