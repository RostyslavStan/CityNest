﻿namespace CityNest
{
    public class Agent
    {
        public Agent(Guid id, string name, string email, string passwordHash, long phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public List<Property>? Properties { get; set; }
    }
}
