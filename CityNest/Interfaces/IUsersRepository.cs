﻿namespace CityNest
{
    public interface IUsersRepository
    {
        public Task<List<User>> Get();
        public Task<User> GetByEmail(string email);
        public Task Add(User user);
        public Task Update(User user);
        public Task Delete(Guid id);
    }
}
